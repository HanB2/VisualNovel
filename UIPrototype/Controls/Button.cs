using System;
using OpenTK.Input;
using OpenTK.Graphics;
using Minalear;

namespace UIPrototype.Controls
{
    public class Button : Image
    {
        private Color4 normalColor;
        private Color4 hoverColor;
        private Color4 pressedColor;

        public Button(string imagePath)
            : base(imagePath)
        {
            normalColor = Color4.White;
            hoverColor = Color4.Blue;
            pressedColor = Color4.Red;
        }

        public override void OnMouseDown(MouseButtonEventArgs e)
        {
            this.DrawColor = pressedColor;
        }
        public override void OnMouseUp(MouseButtonEventArgs e)
        {
            this.DrawColor = hoverColor;
            if (Click != null)
                Click(this, e);
        }
        public override void OnMouseEnter()
        {
            this.DrawColor = hoverColor;
        }
        public override void OnMouseLeave()
        {
            this.DrawColor = normalColor;
        }

        public delegate void ButtonEventDelegate(object sender, MouseButtonEventArgs e);
        public event ButtonEventDelegate Click;

        #region Properties
        public Color4 NormalColor
        {
            get { return this.normalColor; }
            set { this.normalColor = value; }
        }
        public Color4 HoverColor
        {
            get { return this.hoverColor; }
            set { this.hoverColor = value; }
        }
        public Color4 PressedColor
        {
            get { return this.pressedColor; }
            set { this.pressedColor = value; }
        }
        #endregion
    }
}
