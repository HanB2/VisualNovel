using System;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections.Generic;
using OpenTK;
using TK = OpenTK.Graphics;
using TKG = OpenTK.Graphics.OpenGL;
using Minalear;
using Minalear.UI;
using Minalear.UI.Controls;
using OpenTK.Input;

namespace DongLife.Controls
{
    public class MessageBox : Control
    {
        private Texture2D renderTexture;
        private Bitmap renderBitmap;
        private Graphics graphics;
        private Font messageFont;

        private MessageButton[] messageButtons;
        private bool renderingButtons = false;

        private string textBuffer = String.Empty;
        private string currentText = String.Empty;
        private int textIndex = 0;
        private List<RectangleF> characterRegions;

        private float textTimer = 0f;
        private int textDelay = 0;

        private bool cursorDisplayed = true;
        private int cursorDelay = 600;

        public MessageBox(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            characterRegions = new List<RectangleF>();

            textDelay = GameSettings.GetSetting<int>("TextSpeed");
            textTimer = 0f;
            this.DrawOrder = 0f;
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(renderTexture, Bounds, TK.Color4.White);

            base.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            //Not rendering buttons, render text
            if (!renderingButtons && !String.IsNullOrEmpty(textBuffer))
            {
                textTimer += (float)gameTime.ElapsedTime.TotalMilliseconds;
                if (textIndex < textBuffer.Length)
                {
                    if (textTimer >= textDelay)
                    {
                        textTimer = 0f;
                        textIndex++;

                        setText(textBuffer.Substring(0, textIndex));
                    }
                }
                else
                {
                    //Render the click prompt
                    if (textTimer >= cursorDelay)
                    {
                        textTimer = 0f;
                        if (!cursorDisplayed)
                            setText(textBuffer + "▼");
                        else
                            setText(textBuffer.Substring(0, textBuffer.Length - 1));
                        cursorDisplayed = !cursorDisplayed;
                    }
                }
            }
            
            base.Update(gameTime);
        }
        public override void LoadContent(ContentManager content)
        {
            renderBitmap = new Bitmap((int)Width, (int)Height);
            graphics = Graphics.FromImage(renderBitmap);
            messageFont = new Font("Comic Sans MS", 12f);

            fillBackground();
            createTexture();

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            renderTexture.Delete();
            renderBitmap.Dispose();
            graphics.Dispose();
            messageFont.Dispose();

            characterRegions.Clear();

            base.UnloadContent();
        }

        public void SetText(string text)
        {
            textBuffer = text;
            formatText();

            if (ContentLoaded)
            {
                textBuffer = wrapText(textBuffer, (int)Width - 20);
                textIndex = 0;
            }

            renderingButtons = false;
        }
        public void SetButtons(params string[] buttons)
        {
            //Initialize buttons if they were not initialized
            #region ButtonInit
            if (messageButtons == null)
            {
                messageButtons = new MessageButton[6];
                for (int k = 0; k < messageButtons.Length; k++)
                {
                    messageButtons[k] = new MessageButton(k, new Vector2(10, k * 24 + 10), this.Position, new Vector2(Bounds.Width - 20, 24));
                    messageButtons[k].Enabled = false;
                    messageButtons[k].Hover += Button_Hover;
                    messageButtons[k].Click += Button_Click;

                    AddChild(messageButtons[k]);
                }
            }
            #endregion

            renderingButtons = true;

            //Disable all buttons and then enable the number of buttons that we need
            for (int i = 0; i < messageButtons.Length; i++)
            {
                messageButtons[i].Enabled = false;
            }
            for (int i = 0; i < buttons.Length && i < messageButtons.Length; i++)
            {
                messageButtons[i].Enabled = true;
                messageButtons[i].Text = ">> " + buttons[i];
            }
            
            if (ContentLoaded)
                renderButtons();
        }
        public void Clear()
        {
            textBuffer = String.Empty;
            setText(textBuffer);
        }

        public override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (renderingButtons)
                base.OnMouseMove(e);
        }
        public override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (renderingButtons)
                base.OnMouseDown(e);
        }
        public override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (renderingButtons)
                base.OnMouseUp(e);
            else if (!String.IsNullOrEmpty(textBuffer))
            {
                if (textIndex == textBuffer.Length)
                {
                    if (TextFinished != null)
                        TextFinished(this, EventArgs.Empty);
                }
                else
                {
                    //Skip text scrolling
                    textIndex = textBuffer.Length - 1;
                }
            }
        }

        #region Private Methods
        private void formatText()
        {
            //PlayerName
            textBuffer = textBuffer.Replace("{PLAYERNAME}", GameSettings.PlayerName);
        }
        private void setText(string text)
        {
            fillBackground();
            graphics.DrawString(text, messageFont, Brushes.White, 10, 10);

            updateTexture();
        }
        private void updateText()
        {
            graphics.DrawString(textBuffer[textIndex].ToString(), messageFont, Brushes.White, characterRegions[textIndex].Location);
            updateTexture();
        }
        private void calculateRegions(string text)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Near;
            format.Trimming = StringTrimming.EllipsisWord;
            format.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

            //Have to split the text by 32 characters because SetMeasureableCharacterRanges will overflow
            int numberOfChunks = (int)Math.Ceiling(text.Length % 32.0);
            int totalBuffer = text.Length;
            

            for (int chunk = 0; chunk < numberOfChunks && totalBuffer > 0; chunk++)
            {
                int buffer = Math.Min(32, totalBuffer);

                CharacterRange[] ranges = new CharacterRange[buffer];
                for (int i = 0; i < ranges.Length; i++)
                {
                    ranges[i] = new CharacterRange(i, 1);
                }
                format.SetMeasurableCharacterRanges(ranges);

                string bufferText = text.Substring(chunk * 32, buffer);

                Region[] regions = graphics.MeasureCharacterRanges(bufferText, messageFont, new RectangleF(10, 10, Width - 20, Height - 20), format);
                for (int i = 0; i < regions.Length; i++)
                {
                    characterRegions.Add(regions[i].GetBounds(graphics));
                }

                totalBuffer -= buffer;
            }
            format.Dispose();
        }
        private void renderButtons()
        {
            if (renderingButtons)
            {
                fillBackground();
                for (int i = 0; i < messageButtons.Length; i++)
                {
                    if (!messageButtons[i].Enabled)
                        break;
                    messageButtons[i].RenderText(graphics, messageFont);
                }

                updateTexture();
            }
        }
        private void fillBackground()
        {
            HatchBrush fill = new HatchBrush(HatchStyle.Weave, Color.FromArgb(200, 25, 25, 25), Color.FromArgb(200, 10, 10, 10));
            Pen border = new Pen(new LinearGradientBrush(Bounds, Color.White, Color.Black, LinearGradientMode.Vertical), 2f);

            graphics.Clear(Color.Transparent);
            graphics.FillRectangle(fill, 0, 0, Bounds.Width, Bounds.Height);
            graphics.DrawRectangle(border, 2, 2, Bounds.Width - 4, Bounds.Height);
        }
        private void updateTexture()
        {
            BitmapData data = renderBitmap.LockBits(new Rectangle(0, 0, renderBitmap.Width, renderBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            TKG.GL.ActiveTexture(TKG.TextureUnit.Texture0);
            renderTexture.UpdateTexture(data.Scan0);
            renderBitmap.UnlockBits(data);
        }
        private void createTexture()
        {
            BitmapData data = renderBitmap.LockBits(new Rectangle(0, 0, renderBitmap.Width, renderBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            TKG.GL.ActiveTexture(TKG.TextureUnit.Texture0);
            renderTexture = new Texture2D(renderBitmap.Width, renderBitmap.Height, data.Scan0);
            renderBitmap.UnlockBits(data);
        }
        private string wrapText(string text, int maxWidth)
        {
            StringBuilder stringBuffer = new StringBuilder(text.Length);
            string[] lines = text.Split('\n');

            foreach (string line in lines)
            {
                if (graphics.MeasureString(line, messageFont).Width > maxWidth)
                {
                    string[] words = line.Split(' ');
                    float bufferWidth = 0f;

                    foreach (string word in words)
                    {
                        SizeF size = graphics.MeasureString(word + " ", messageFont, maxWidth, 
                            new StringFormat(StringFormat.GenericTypographic) { FormatFlags = StringFormatFlags.MeasureTrailingSpaces });
                        if (size.Width + bufferWidth < maxWidth)
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
                    stringBuffer.Append("\n");
                }
                else
                {
                    stringBuffer.Append(line + "\n");
                }
            }

            return stringBuffer.ToString();
        }
        private void Button_Hover(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < messageButtons.Length; i++)
                messageButtons[i].Selected = false;

            messageButtons[(int)sender].Selected = true;
            renderButtons();
        }
        private void Button_Click(object sender, MouseButtonEventArgs e)
        {
            if (OptionSelected != null)
                OptionSelected(this, (int)sender);
        }
        #endregion

        public string Text
        {
            get { return this.textBuffer; } 
            set { this.SetText(value); }
        }
        public string TextRaw
        {
            get { return this.textBuffer; }
            set { this.setText(value); }
        }

        public delegate void TextFinishedDelegate(object sender, EventArgs args);
        public delegate void MessageBoxOptionSelectedDelegate(object sender, int optionID);
        public event MessageBoxOptionSelectedDelegate OptionSelected;
        public event TextFinishedDelegate TextFinished;
    }
}
