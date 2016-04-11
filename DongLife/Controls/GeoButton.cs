using OpenTK.Input;
using OpenTK.Graphics;
using Minalear;
using Minalear.UI.Controls;

namespace DongLife.Controls
{
    public class GeoButton : Control, IGeoDrawable
    {
        private ButtonModes currentMode = ButtonModes.Normal;

        void IGeoDrawable.Draw(GeoRenderer renderer)
        {
            //Fill
            if (currentMode == ButtonModes.Normal)
                renderer.FillRect(Position, Size, new Color4(0.3f, 0.3f, 0.3f, 1f));
            else if (currentMode == ButtonModes.Hover)
                renderer.FillRect(Position, Size, new Color4(0.5f, 0.5f, 0.5f, 1f));
            else //Pressed
                renderer.FillRect(Position, Size, new Color4(0.2f, 0.2f, 0.2f, 1f));

            //Border
            renderer.DrawRect(Position, Size, Color4.White);
        }
        void IGeoDrawable.Update(GameTime gameTime) { }

        public override void OnMouseEnter()
        {
            currentMode = ButtonModes.Hover;
        }
        public override void OnMouseLeave()
        {
            currentMode = ButtonModes.Normal;
        }
        public override void OnKeyDown(object sender, KeyboardKeyEventArgs e)
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
