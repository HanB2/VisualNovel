using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using OpenTK;
using OpenTK.Graphics;
using TKG = OpenTK.Graphics.OpenGL;
using Minalear;
using Minalear.UI;
using Minalear.UI.Controls;

namespace DongLife.Controls
{
    public class FatigueBar : Control
    {
        private ControlAnimator animator;

        private Texture2D texture;
        private Bitmap renderBitmap;
        private Graphics graphics;

        public FatigueBar() : base(new RectangleF(0f, 0f, 300f, 24f))
        {
            this.DrawOrder = 0f;
            this.animator = new ControlAnimator();
            this.AddChild(animator);
        }

        public override void LoadContent(ContentManager content)
        {
            renderBitmap = new Bitmap((int)Width, (int)Height);
            graphics = Graphics.FromImage(renderBitmap);

            createTexture();

            base.LoadContent(content);
        }

        public override void UnloadContent()
        {
            texture.Dispose();
            renderBitmap.Dispose();
            graphics.Dispose();

            base.UnloadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds, DrawColor);

            base.Draw(spriteBatch);
        }

        public void SetAmount(float percent)
        {
            if (ContentLoaded)
            {
                setTexture(percent);
            }
        }

        private void setTexture(float percent)
        {
            Brush brush = new LinearGradientBrush(new RectangleF(0, 0, Width, Height), Color.Red, Color.Green, LinearGradientMode.Horizontal);
            Font font = new Font("Comic Sans MS", 10f);

            graphics.Clear(Color.Transparent);
            graphics.FillRectangle(brush, new RectangleF(0, 0, Width * percent, Height));
            graphics.DrawString("Fatigue", font, Brushes.Black, 0f, 0f);

            font.Dispose();
            brush.Dispose();

            BitmapData data = renderBitmap.LockBits(new Rectangle(0, 0, renderBitmap.Width, renderBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            TKG.GL.ActiveTexture(TKG.TextureUnit.Texture0);
            texture.UpdateTexture(data.Scan0);
            renderBitmap.UnlockBits(data);
        }
        private void createTexture()
        {
            Brush brush = new LinearGradientBrush(new RectangleF(0, 0, Width, Height), Color.Red, Color.Green, LinearGradientMode.Horizontal);
            Font font = new Font("Comic Sans MS", 10f);

            graphics.Clear(Color.Transparent);
            graphics.FillRectangle(brush, new RectangleF(0, 0, Width, Height));
            graphics.DrawString("Fatigue", font, Brushes.Black, 0f, 0f);

            font.Dispose();
            brush.Dispose();

            BitmapData data = renderBitmap.LockBits(new Rectangle(0, 0, renderBitmap.Width, renderBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            TKG.GL.ActiveTexture(TKG.TextureUnit.Texture0);
            texture = new Texture2D(renderBitmap.Width, renderBitmap.Height, data.Scan0);
            renderBitmap.UnlockBits(data);
        }

        public ControlAnimator Animator
        {
            get { return this.animator; }
            set { this.animator = value; }
        }
    }
}
