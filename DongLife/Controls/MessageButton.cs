using System.Drawing;
using OpenTK;
using Minalear;
using Minalear.UI.Controls;
using OpenTK.Input;

namespace DongLife.Controls
{
    //TODO: Optimize the text rendering
    public class MessageButton : Control
    {
        public string Text { get; set; }
        public bool Selected { get; set; }

        private int id;
        private Vector2 localPos;

        public MessageButton(int id, Vector2 position, Vector2 parentPos, Vector2 size)
        {
            this.id = id;
            Bounds = new RectangleF(parentPos.X + position.X, parentPos.Y + position.Y, size.X, size.Y);
            localPos = position;
        }

        public void RenderText(Graphics graphics, Font font)
        {
            if (Selected)
                graphics.DrawString(this.Text, font, Brushes.Red, localPos.X, localPos.Y);
            else
                graphics.DrawString(this.Text, font, Brushes.White, localPos.X, localPos.Y);
        }

        public override void OnMouseEnter()
        {
            if (Hover != null)
                Hover(this.id, null);
            
            base.OnMouseEnter();
        }
        public override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (Click != null)
                Click(this.id, e);

            base.OnMouseUp(e);
        }

        public event ButtonPressedDelegate Click;
        public event ButtonPressedDelegate Hover;
    }
}
