using OpenTK;
using OpenTK.Graphics;
using Minalear.UI.Controls;
using Minalear;

namespace DongLife.Code
{
    public class Background : Control
    {
        private string backgroundPath;
        private Texture2D backgroundTexture;

        public Background(string texturePath)
        {
            backgroundPath = texturePath;
            this.DrawOrder = 1f;
            this.AutoSize = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, Bounds, this.DrawColor, 0f, Vector2.Zero, RenderFlags.Blur | RenderFlags.Desaturate);

            base.Draw(spriteBatch);
        }

        public override void LoadContent(ContentManager content)
        {
            backgroundTexture = content.LoadTexture2D(backgroundPath);

            if (AutoSize)
            {
                Position = Vector2.Zero;
                Width = GameSettings.WindowWidth;
                Height = GameSettings.WindowHeight;
            }

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            backgroundTexture.Delete();

            base.UnloadContent();
        }

        public bool AutoSize { get; set; }
    }
}
