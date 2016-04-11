using OpenTK;
using Minalear;
using Minalear.UI.Controls;

namespace DongLife.Controls
{
    public class CreatorOptionSelector : Control, IGeoDrawable
    {
        private LabelBox labelBox;
        private GeoButton left, right;

        public CreatorOptionSelector(string text, Vector2 position, float width)
        {
            Position = position;
            Size = new Vector2(width, 48f);

            left = new GeoButton();
            left.Size = new Vector2(32f, 32f);
            left.Position = new Vector2(position.X + 8f, position.Y + 8f);

            right = new GeoButton();
            right.Size = new Vector2(32f, 32f);
            right.Position = new Vector2(position.X + width - 40f, position.Y + 8f);

            labelBox = new LabelBox(width, 48, text);
            labelBox.Position = position;

            AddChild(labelBox);

            DrawOrder = 0f;

            left.ButtonPressed += LeftPressed;
            right.ButtonPressed += RightPressed;
        }

        void IGeoDrawable.Draw(GeoRenderer renderer)
        {
            (left as IGeoDrawable).Draw(renderer);
            (right as IGeoDrawable).Draw(renderer);
        }
        void IGeoDrawable.Update(GameTime gameTime)
        {
            (left as IGeoDrawable).Update(gameTime);
            (right as IGeoDrawable).Update(gameTime);
        }

        private void LeftPressed(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(this, -1);
        }
        private void RightPressed(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(this, 1);
        }

        public delegate void OptionSelectionChangedDelegate(object sender, int value);
        public event OptionSelectionChangedDelegate SelectionChanged;
    }
}
