using System;
using System.Text;
using System.Drawing;
using Imaging = System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Minalear;

namespace VisualNovelTest
{
    public class TextBox
    {
        private Font font;
        private RectangleF rectangle;
        private Texture2D renderTarget;

        private string textBuffer;
        private int textIndex = 0;

        private float textCrawlTimer = 0f;
        private float textDelay = 30f;

        public TextBox()
        {
            font = new Font(FontFamily.GenericMonospace, 12f);
            rectangle = new RectangleF(0, 0, 600, 100);
            renderTarget = new Texture2D(1, 1, IntPtr.Zero);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(renderTarget, rectangle, Color.White);
        }
        public void Update(GameTime gameTime)
        {
            if (textIndex < textBuffer.Length)
            {
                textCrawlTimer += (float)gameTime.ElapsedTime.TotalMilliseconds;
                if (textCrawlTimer >= textDelay)
                {
                    this.setText(textBuffer.Substring(0, textIndex + 1));
                    textIndex++;
                    textCrawlTimer = 0f;
                }
            }
        }

        public void SetText(string text)
        {
            textBuffer = text;
            textIndex = 0;
        }
        private void setText(string text)
        {
            renderTarget.Delete(); //Delete texture
            Bitmap bmp = new Bitmap((int)rectangle.Width, (int)rectangle.Height);
            Graphics graphics = Graphics.FromImage(bmp);

            Brush brush = new SolidBrush(Color.FromArgb(200, 45, 45, 45));

            RectangleF rectF = new RectangleF(10f, 10f, rectangle.Width - 20f, rectangle.Height - 20f);

            graphics.FillRectangle(brush, rectangle);
            graphics.DrawRectangle(Pens.White, 0, 0, rectangle.Width - 1, rectangle.Height);
            graphics.DrawString(text, font, Brushes.White, rectF, StringFormat.GenericDefault);

            Imaging.BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), Imaging.ImageLockMode.ReadOnly, Imaging.PixelFormat.Format32bppArgb);

            GL.ActiveTexture(TextureUnit.Texture0);
            renderTarget = new Texture2D(bmp.Width, bmp.Height, data.Scan0);
            bmp.UnlockBits(data);

            graphics.Dispose();
            bmp.Dispose();
        }

        [Obsolete()]
        private string wrapText(Graphics graphics, string text)
        {
            StringBuilder stringBuffer = new StringBuilder(text.Length);
            string[] lines = text.Split('\n');

            
            foreach (string line in lines)
            {
                if (graphics.MeasureString(line, font).Width > rectangle.Width)
                {
                    string[] words = line.Split(' ');
                    float bufferWidth = 0f;

                    foreach (string word in words)
                    {
                        SizeF size = graphics.MeasureString(word, font, (int)rectangle.Width, StringFormat.GenericTypographic);
                        if (size.Width + bufferWidth < rectangle.Width - 20)
                        {
                            bufferWidth += size.Width;
                            stringBuffer.Append(word + " ");
                        }
                        else
                        {
                            bufferWidth = size.Width;
                            stringBuffer.Append("\n" + word + " ");
                        }
                    }
                }
                else
                {
                    stringBuffer.Append(line + "\n");
                }
            }

            return stringBuffer.ToString();
        }
    }
}
