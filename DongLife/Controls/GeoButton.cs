using OpenTK.Input;
using OpenTK.Graphics;
using Minalear;
using Minalear.UI.Controls;

namespace DongLife.Controls
{
    public class GeoButton : Control
    {
        private ButtonModes currentMode = ButtonModes.Normal;

        public GeoButton()
        {
            DrawOrder = 0.4f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            GameManager.Renderer.Begin();
            //Fill
            if (currentMode == ButtonModes.Normal)
                GameManager.Renderer.FillRect(Position, Size, new Color4(0.3f, 0.3f, 0.3f, 1f));
            else if (currentMode == ButtonModes.Hover)
                GameManager.Renderer.FillRect(Position, Size, new Color4(0.5f, 0.5f, 0.5f, 1f));
            else //Pressed
                GameManager.Renderer.FillRect(Position, Size, new Color4(0.2f, 0.2f, 0.2f, 1f));

            //Border
            GameManager.Renderer.DrawRect(Position, Size, Color4.White);
            GameManager.Renderer.End();
        }

        public override void OnMouseEnter()
        {
            currentMode = ButtonModes.Hover;
        }
        public override void OnMouseLeave()
        {
            currentMode = ButtonModes.Normal;
        }
        public override void OnMouseDown(MouseButtonEventArgs e)
        {
            currentMode = ButtonModes.Pressed;
        }
        public override void OnMouseUp(MouseButtonEventArgs e)
        {
            currentMode = ButtonModes.Hover;
            if (ButtonPressed != null)
                ButtonPressed(this, e);
        }

        public event ButtonPressedDelegate ButtonPressed;
        public enum ButtonModes { Normal, Hover, Pressed }
    }

    public delegate void ButtonPressedDelegate(object sender, MouseButtonEventArgs e);
}
