using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using Minalear;
using Minalear.UI.Controls;

namespace DongLife.Controls
{
    public class Button : Image
    {
        private Color4 normalColor, pressedColor, hoverColor;

        public Button(string imagePath)
            : base(imagePath)
        {
            normalColor = Color4.LightGray;
            pressedColor = Color4.Gray;
            hoverColor = Color4.White;

            DrawColor = normalColor;
        }

        public override void OnMouseDown(MouseButtonEventArgs e)
        {
            DrawColor = pressedColor;

            base.OnMouseDown(e);
        }
        public override void OnMouseUp(MouseButtonEventArgs e)
        {
            DrawColor = hoverColor;

            if (Click != null)
                Click(this, e);

            base.OnMouseUp(e);
        }
        public override void OnMouseEnter()
        {
            DrawColor = hoverColor;

            base.OnMouseEnter();
        }
        public override void OnMouseLeave()
        {
            DrawColor = normalColor;

            base.OnMouseLeave();
        }
        
        public event ButtonEventDelegate Click;

        #region Colors
        public Color4 NormalColor
        {
            get { return normalColor; }
            set
            {
                normalColor = value;
                DrawColor = value;
            }
        }
        public Color4 PressedColor
        {
            get { return pressedColor; }
            set { pressedColor = value; }
        }
        public Color4 HoverColor
        {
            get { return hoverColor; }
            set { hoverColor = value; }
        }
        #endregion
    }

    public delegate void ButtonEventDelegate(object sender, MouseButtonEventArgs e);
}
