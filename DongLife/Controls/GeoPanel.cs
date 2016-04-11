using OpenTK.Input;
using OpenTK.Graphics;
using Minalear;
using Minalear.UI.Controls;

namespace DongLife.Controls
{
    public class GeoPanel : Control, IGeoDrawable
    {
        void IGeoDrawable.Draw(GeoRenderer renderer)
        {
            renderer.FillRect(Position, Size, new Color4(0.2f, 0.2f, 0.2f, 0.8f));
            renderer.DrawRect(Position, Size, Color4.White);
        }
        void IGeoDrawable.Update(GameTime gameTime) { }
    }
}
