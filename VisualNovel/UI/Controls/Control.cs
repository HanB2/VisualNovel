using System;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;

namespace Minalear.UI.Controls
{
    public class Control : IDrawable, IDisposable, IComparable
    {
        private Control parent;
        private List<Control> children;
        private RectangleF bounds;
        private Color4 drawColor = Color4.White;

        private bool contentLoaded = false;
        private bool visible = true;
        private bool enabled = true;
        private float drawOrder = 1f;

        public Control()
        {
            children = new List<Control>();
        }
        public Control(RectangleF bounds)
        {
            this.bounds = bounds;
            children = new List<Control>();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].Visible && children[i].ContentLoaded)
                    children[i].Draw(spriteBatch);
            }
        }
        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].Enabled && children[i].ContentLoaded)
                    children[i].Update(gameTime);
            }
        }
        public virtual void LoadContent(ContentManager content)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (!children[i].ContentLoaded)
                    children[i].LoadContent(content);
            }
            contentLoaded = true;
        }
        public virtual void UnloadContent()
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].ContentLoaded)
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
        #region KeyboardEvents
        public virtual void OnKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].enabled)
                    children[i].OnKeyDown(sender, e);
            }
        }
        public virtual void OnKeyUp(object sender, KeyboardKeyEventArgs e)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].enabled)
                    children[i].OnKeyUp(sender, e);
            }
        }
        public virtual void OnKeyPress(object sender, OpenTK.KeyPressEventArgs e)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].enabled)
                    children[i].OnKeyPress(sender, e);
            }
        }
        #endregion

        public void AddChild(Control control)
        {
            control.Parent = this;
            children.Add(control);

            //Drawing sort
            children.Sort();
            children.Reverse();
        }
        public void RemoveChild(Control control)
        {
            control.Parent = null;
            children.Remove(control);
        }

        public int CompareTo(object obj)
        {
            return this.DrawOrder.CompareTo(((Control)obj).DrawOrder);
        }

        #region Properties
        public Control Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }
        public List<Control> Children
        {
            get { return this.children; }
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
        public Color4 DrawColor
        {
            get { return this.drawColor; }
            set { this.drawColor = value; }
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
            protected set { this.contentLoaded = value; }
        }
        public bool Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }
        public bool Enabled
        {
            get { return this.enabled; }
            set { this.enabled = value; }
        }
        public float DrawOrder
        {
            get { return this.drawOrder; }
            set { this.drawOrder = value; }
        }
        #endregion
    }
}
