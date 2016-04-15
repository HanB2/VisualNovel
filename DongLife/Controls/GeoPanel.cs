using OpenTK.Input;
using OpenTK.Graphics;
using Minalear;
using Minalear.UI.Controls;

namespace DongLife.Controls
{
    public class GeoPanel : Control
    {
        public GeoPanel()
        {
            DrawOrder = 1f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            GameManager.Renderer.Begin();
            GameManager.Renderer.FillRect(Position, Size, new Color4(0.2f, 0.2f, 0.2f, 0.8f));
            GameManager.Renderer.DrawRect(Position, Size, Color4.White);
            GameManager.Renderer.End();
        }
    }
}
