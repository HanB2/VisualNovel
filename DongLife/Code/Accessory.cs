using OpenTK;
using OpenTK.Graphics;
using Minalear;

namespace DongLife.Code
{
    public class Accessory
    {
        private Vector2 offset;
        private Texture2D texture;
        private string texturePath;

        public Accessory(string texturePath, Vector2 offset)
        {
            this.texturePath = texturePath;
            this.offset = offset;
        }

        public void Draw(SpriteBatch spriteBatch, Actor parent)
        {
            if (!string.IsNullOrWhiteSpace(texturePath))
            {
                if (parent.HasFocus)
                {
                    spriteBatch.Draw(texture, parent.Position + offset, Color4.White, 0f,
                        new Vector2(0.5f, 0.5f), parent.CurrentScale, RenderFlags.None);
                }
                else
                {
                    spriteBatch.Draw(texture, parent.Position + offset, Color4.White, 0f, 
                        new Vector2(0.5f, 0.5f), parent.CurrentScale, RenderFlags.Blur | RenderFlags.Desaturate);
                }
            }
        }
        public void Update(GameTime gameTime) { }

        public void LoadContent(ContentManager content)
        {
            texture = content.LoadTexture2D(texturePath);
        }
        public void UnloadContent()
        {
            texture.Dispose();
        }
    }
}
