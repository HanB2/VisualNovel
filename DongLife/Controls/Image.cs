using OpenTK.Graphics;
using Minalear;
using Minalear.UI.Controls;

namespace DongLife.Controls
{
    public class Image : Control
    {
        private string imagePath;
        private Texture2D imageTexture;

        public Image(string imagePath)
        {
            this.imagePath = imagePath;
            this.AutoSize = true;
            this.DrawOrder = 0.2f;
        }

        public override void LoadContent(ContentManager content)
        {
            imageTexture = content.LoadTexture2D(imagePath);

            if (AutoSize)
                Size = new OpenTK.Vector2(imageTexture.Width, imageTexture.Height);

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            imageTexture.Delete();

            base.UnloadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(imageTexture, Bounds, this.DrawColor);

            base.Draw(spriteBatch);
        }

        public string ImagePath
        {
            get { return this.imagePath; }
            set { this.imagePath = value; }
        }
        public bool AutoSize { get; set; }
    }
}
