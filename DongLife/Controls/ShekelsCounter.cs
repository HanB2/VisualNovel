using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics;
using TKG = OpenTK.Graphics.OpenGL;
using Minalear;
using Minalear.UI;
using Minalear.UI.Controls;

namespace DongLife.Controls
{
    public class ShekelsCounter : Control
    {
        private Texture2D backgroundTexture;
        private Texture2D textTexture;

        private Bitmap renderBitmap;
        private Graphics graphics;
        private Font textFont;

        private ControlAnimator animator;

        public ShekelsCounter() : base(new RectangleF(0f, 0f, 141, 86))
        {
            this.DrawOrder = 0f;
            this.animator = new ControlAnimator();

            this.AddChild(animator);
        }

        public override void LoadContent(ContentManager content)
        {
            backgroundTexture = content.LoadTexture2D(@"Textures/Props/shekels_hud.png");
            renderBitmap = new Bitmap((int)Width, (int)Height);

            graphics = Graphics.FromImage(renderBitmap);
            textFont = new Font("Comic Sans MS", 14f);

            graphics.DrawString("Shekels: 0", textFont, Brushes.White, 15f, 29f);
            createTexture();

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            backgroundTexture.Dispose();
            textTexture.Dispose();

            renderBitmap.Dispose();
            graphics.Dispose();
            textFont.Dispose();

            base.UnloadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, this.Position, this.DrawColor);
            spriteBatch.Draw(textTexture, this.Position, this.DrawColor);

            base.Draw(spriteBatch);
        }

        public void SetAmount(int amount)
        {
            if (ContentLoaded)
            {
                graphics.Clear(Color.Transparent);
                graphics.DrawString("Shekels: " + amount.ToString(), 
                    textFont, Brushes.White, 15f, 29f);
                updateTexture();
            }
        }
        private void createTexture()
        {
            BitmapData data = renderBitmap.LockBits(new Rectangle(0, 0, renderBitmap.Width, renderBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            TKG.GL.ActiveTexture(TKG.TextureUnit.Texture0);
            textTexture = new Texture2D(renderBitmap.Width, renderBitmap.Height, data.Scan0);
            renderBitmap.UnlockBits(data);
        }
        private void updateTexture()
        {
            BitmapData data = renderBitmap.LockBits(new Rectangle(0, 0, renderBitmap.Width, renderBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            TKG.GL.ActiveTexture(TKG.TextureUnit.Texture0);
            textTexture.UpdateTexture(data.Scan0);
            renderBitmap.UnlockBits(data);
        }

        public ControlAnimator Animator
        {
            get { return this.animator; }
            set { this.animator = value; }
        }
    }
}
