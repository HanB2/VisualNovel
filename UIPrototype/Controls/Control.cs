using System;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using Minalear;

namespace UIPrototype.Controls
{
    public class Control : IDrawable, IDisposable
    {
        private Control parent;
        protected List<Control> children;
        private RectangleF bounds;

        private bool contentLoaded = false;
        private bool enabled = true;

        public Control()
        {
            children = new List<Control>();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].contentLoaded)
                    children[i].Draw(spriteBatch);
            }
        }
        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].contentLoaded)
                    children[i].Update(gameTime);
            }
        }

        public virtual void LoadContent(ContentManager content)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (!children[i].contentLoaded)
                    children[i].LoadContent(content);
            }
            contentLoaded = true;
        }
        public virtual void UnloadContent()
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].contentLoaded)
                    children[i].UnloadContent();
            }
            contentLoaded = false;
        }
        public virtual void Dispose()
        {
            for (int i = 0; i < children.Count; i++)
                children[i].Dispose();
            UnloadContent();
            contentLoaded = false;
        }

        #region MouseEvents
        public virtual void OnMouseMove(MouseMoveEventArgs e)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].enabled)
                {
                    children[i].OnMouseMove(e);

                    PointF previousPoint = new PointF(e.X - e.XDelta, e.Y - e.YDelta);
                    if (children[i].Bounds.Contains(e.X, e.Y) && !children[i].Bounds.Contains(previousPoint))
                    {
                        children[i].OnMouseEnter();
                    }
                    else if (!children[i].Bounds.Contains(e.X, e.Y) && children[i].Bounds.Contains(previousPoint))
                    {
                        children[i].OnMouseLeave();
                    }
                }
            }
        }
        public virtual void OnMouseDown(MouseButtonEventArgs e)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].enabled)
                {
                    if (children[i].Bounds.Contains(e.X, e.Y))
                        children[i].OnMouseDown(e);
                }
            }
        }
        public virtual void OnMouseUp(MouseButtonEventArgs e)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].enabled)
                {
                    if (children[i].Bounds.Contains(e.X, e.Y))
                        children[i].OnMouseUp(e);
                }
            }
        }
        public virtual void OnMouseEnter() { }
        public virtual void OnMouseLeave() { }
        public virtual void OnMouseWheel(MouseWheelEventArgs e)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].enabled)
                {
                    if (children[i].Bounds.Contains(e.X, e.Y))
                        children[i].OnMouseWheel(e);
                }
            }
        }
        #endregion

        public void AddChild(Control control)
        {
            control.Parent = this;
            children.Add(control);
        }

        #region Properties
        public Control Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }
        public RectangleF Bounds
        {
            get { return this.bounds; }
            set { this.bounds = value; }
        }
        public Vector2 Position
        {
            get { return new Vector2(bounds.X, bounds.Y); }
            set
            {
                bounds.X = value.X;
                bounds.Y = value.Y;
            }
        }
        public float PosX
        {
            get { return bounds.X; }
            set { bounds.X = value; }
        }
        public float PosY
        {
            get { return bounds.Y; }
            set { bounds.Y = value; }
        }
        public Vector2 Size
        {
            get { return new Vector2(bounds.Width, bounds.Height); }
            set
            {
                bounds.Width = value.X;
                bounds.Height = value.Y;
            }
        }
        public float Width
        {
            get { return bounds.Width; }
            set { bounds.Width = value; }
        }
        public float Height
        {
            get { return bounds.Height; }
            set { bounds.Height = value; }
        }
        public bool ContentLoaded
        {
            get { return this.contentLoaded; }
        }
        public bool Enabled
        {
            get { return this.enabled; }
            set { this.enabled = value; }
        }
        #endregion
    }
}
