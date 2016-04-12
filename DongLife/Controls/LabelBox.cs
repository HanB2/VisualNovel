using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using Minalear;
using Minalear.UI;
using Minalear.UI.Controls;
using DongLife.Code;

namespace DongLife.Controls
{
    public class LabelBox : Control
    {
        private string labelText;

        private Bitmap bitmap;
        private Graphics graphics;
        private Texture2D renderTexture;
        private Font font;

        public LabelBox(float width, float height, string text)
        {
            Size = new Vector2(width, height);
            labelText = text;
            this.DrawOrder = 0.5f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(renderTexture, Bounds, Color4.White);

            base.Draw(spriteBatch);
        }
        public override void LoadContent(ContentManager content)
        {
            bitmap = new Bitmap((int)Width, (int)Height);
            graphics = Graphics.FromImage(bitmap);
            font = new Font("Comic Sans MS", 24f, FontStyle.Regular);

            initTexture();

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            bitmap.Dispose();
            graphics.Dispose();
            renderTexture.Dispose();
            font.Dispose();

            base.UnloadContent();
        }
        public virtual void SetText(string text)
        {
            this.labelText = text;
            if (ContentLoaded)
                this.updateTexture();
        }

        private void initTexture()
        {
            graphics.Clear(Color.FromArgb(new Color4(0f, 0f, 0f, 0.5f).ToArgb()));

            if (!string.IsNullOrWhiteSpace(labelText))
            {
                float labelWidth = graphics.MeasureString(labelText, font).Width;
                graphics.DrawString(labelText, font, Brushes.White, Bounds.Width / 2 - labelWidth / 2, 0f);
            }
            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            renderTexture = new Texture2D(bitmap.Width, bitmap.Height, data.Scan0);

            bitmap.UnlockBits(data);
        }
        private void updateTexture()
        {
            graphics.Clear(Color.FromArgb(new Color4(0f, 0f, 0f, 0.5f).ToArgb()));
            float labelWidth = graphics.MeasureString(labelText, font).Width;
            graphics.DrawString(labelText, font, Brushes.White, Bounds.Width / 2 - labelWidth / 2, 0f);

            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            renderTexture.UpdateTexture(data.Scan0);

            bitmap.UnlockBits(data);
        }
    }
}
