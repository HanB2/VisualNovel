using System;
using System.Drawing;
using System.Drawing.Imaging;
using Minalear;
using Minalear.UI;

namespace DongLife.Scenes
{
    public class EndScene : Scene
    {
        private bool goodEnding;
        private Texture2D background;
        private Font font;
        private string message;

        public EndScene(string sceneName, bool goodEnding, string endingText) 
            : base(sceneName)
        {
            this.goodEnding = goodEnding;
            this.message = endingText;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, OpenTK.Vector2.Zero, OpenTK.Graphics.Color4.White);

            base.Draw(spriteBatch);
        }

        public override void LoadContent(ContentManager content)
        {
            font = new Font("Comic Sans MS", 24f);

            using (Bitmap bmp = new Bitmap(GameSettings.WindowWidth, GameSettings.WindowHeight))
            {
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    if (goodEnding)
                        gfx.FillRectangle(Brushes.MediumPurple, 0, 0, bmp.Width, bmp.Height);
                    else
                        gfx.FillRectangle(Brushes.Crimson, 0, 0, bmp.Width, bmp.Height);

                    gfx.DrawString(
                        message, font, Brushes.White, 
                        new RectangleF(40, 40, bmp.Width - 80, bmp.Height - 80), 
                        new StringFormat(StringFormatFlags.MeasureTrailingSpaces)
                        { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }

                BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                background = new Texture2D(bmp.Width, bmp.Height, data.Scan0);
                bmp.UnlockBits(data);
            }

            base.LoadContent(content);
        }

        public override void UnloadContent()
        {
            font.Dispose();
            background.Dispose();

            base.UnloadContent();
        }
    }
}
