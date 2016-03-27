using System;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Input;
using TK = OpenTK.Graphics;
using TKG = OpenTK.Graphics.OpenGL;
using Minalear;
using Minalear.UI.Controls;

namespace DongLife.Controls
{
    public class TextInput : Control
    {
        private Texture2D renderTexture;
        private Bitmap renderBitmap;
        private Graphics graphics;
        private Font textFont;

        private string textBuffer;
        private const int MAX_TEXT_BUFFER = 25;

        public TextInput(int width, int height)
        {
            Size = new Vector2(width, height);
            DrawOrder = 0f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(renderTexture, Bounds, TK.Color4.White);

            base.Draw(spriteBatch);
        }

        public override void LoadContent(ContentManager content)
        {
            renderBitmap = new Bitmap((int)Width, (int)Height);
            graphics = Graphics.FromImage(renderBitmap);
            textFont = new Font("Comic Sans MS", 12f);

            textBuffer = String.Empty;

            fillBackground();
            createTexture();

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            renderTexture.Dispose();
            renderBitmap.Dispose();
            graphics.Dispose();
            textFont.Dispose();

            textBuffer = String.Empty;

            base.UnloadContent();
        }

        public void UpdateTextBox(char ch)
        {
            if (textBuffer.Length < MAX_TEXT_BUFFER)
            {
                textBuffer += ch;
                drawText();
            }
        }
        public void SetTextBox(string str)
        {
            if (str.Length < MAX_TEXT_BUFFER)
                textBuffer = str;
            else
                textBuffer = str.Substring(0, MAX_TEXT_BUFFER);

            drawText();
        }
        public void Backspace()
        {
            if (textBuffer.Length > 0)
            {
                textBuffer = textBuffer.Substring(0, textBuffer.Length - 1);
                drawText();
            }
        }

        public override void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            UpdateTextBox(e.KeyChar); 
        }
        public override void OnKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.BackSpace)
                Backspace();
            else if (e.Key == Key.Enter || e.Key == Key.KeypadEnter)
                SubmitText();
        }
        public void SubmitText()
        {
            if (OnSubmitText != null)
            {
                OnSubmitText(this, textBuffer);
                Clear();
            }
        }
        public void Clear()
        {
            textBuffer = String.Empty;
            drawText();
        }

        private void fillBackground()
        {
            Brush fill = Brushes.Black;
            Pen border = Pens.White;

            graphics.Clear(Color.Transparent);
            graphics.FillRectangle(fill, 0, 0, Width, Height);
            graphics.DrawRectangle(border, 1, 1, Width - 3, Height - 3);
        }
        private void createTexture()
        {
            BitmapData data = renderBitmap.LockBits(new Rectangle(0, 0, renderBitmap.Width, renderBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            TKG.GL.ActiveTexture(TKG.TextureUnit.Texture0);
            renderTexture = new Texture2D(renderBitmap.Width, renderBitmap.Height, data.Scan0);

            renderBitmap.UnlockBits(data);
        }
        private void updateTexture()
        {
            BitmapData data = renderBitmap.LockBits(new Rectangle(0, 0, renderBitmap.Width, renderBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            TKG.GL.ActiveTexture(TKG.TextureUnit.Texture0);
            renderTexture.UpdateTexture(data.Scan0);

            renderBitmap.UnlockBits(data);
        }
        private void drawText()
        {
            fillBackground();
            graphics.DrawString(textBuffer, textFont, Brushes.White, 3f, 3f);
            updateTexture();
        }

        public delegate void SubmitTextDelegate(object sender, string text);
        public event SubmitTextDelegate OnSubmitText;
    }
}
