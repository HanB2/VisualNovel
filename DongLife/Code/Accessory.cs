using System;
using OpenTK;
using OpenTK.Graphics;
using Minalear;

namespace DongLife.Code
{
    public class Accessory : IComparable
    {
        private Vector2 offset;
        private Texture2D texture;
        private string texturePath;
        private int drawOrder = 0;

        public Accessory(string texturePath, Vector2 offset, int drawOrder)
        {
            this.texturePath = texturePath;
            this.offset = offset;
            this.drawOrder = drawOrder;
        }

        public void Draw(SpriteBatch spriteBatch, Actor parent)
        {
            if (!string.IsNullOrWhiteSpace(texturePath))
            {
                Color4 drawColor = Color4.White;
                drawColor.A = parent.DrawColor.A;

                if (parent.HasFocus)
                {
                    spriteBatch.Draw(texture, parent.Position + offset * parent.CurrentScale, drawColor, 0f,
                        new Vector2(0.5f, 0.5f), parent.CurrentScale, RenderFlags.None);
                }
                else
                {
                    spriteBatch.Draw(texture, parent.Position + offset * parent.CurrentScale, drawColor, 0f, 
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

        public int CompareTo(object obj)
        {
            return this.drawOrder.CompareTo(((Accessory)obj).drawOrder);
        }
    }
}
