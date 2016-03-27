using System;
using OpenTK.Graphics;
using OpenTK.Input;
using Minalear;

namespace UIPrototype.Controls
{
    public class Image : Control
    {
        private string imagePath;
        private Texture2D imageTexture;
        private Color4 drawColor;

        public Image(string imagePath)
        {
            this.imagePath = imagePath;
            this.drawColor = Color4.White;
        }

        public override void LoadContent(ContentManager content)
        {
            imageTexture = content.LoadTexture2D(imagePath);
            //Bounds = new System.Drawing.RectangleF(0, 0, imageTexture.Width, imageTexture.Height);
            Width = imageTexture.Width;
            Height = imageTexture.Height;

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            imageTexture.Delete();

            base.UnloadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(imageTexture, Bounds, drawColor);

            base.Draw(spriteBatch);
        }

        public Color4 DrawColor
        {
            get { return this.drawColor; }
            set { this.drawColor = value; }
        }
    }
}
