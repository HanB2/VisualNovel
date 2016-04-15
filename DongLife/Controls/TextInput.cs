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
    public class TextInput : LabelBox
    {
        private string buffer = string.Empty;
        private const int MAX_BUFFER = 32;

        public TextInput(float width, float height)
            : base(width, height, "NAME") { }

        public override void UnloadContent()
        {
            buffer = string.Empty;
            this.SetText(string.Empty);

            base.UnloadContent();
        }
        public override void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (buffer.Length < MAX_BUFFER)
            {
                buffer += e.KeyChar;
                SetText(buffer);
            }
        }
        public override void OnKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            //Allow backspacing
            if (e.Key == Key.BackSpace && buffer.Length > 0)
            {
                buffer = buffer.Substring(0, buffer.Length - 1);
                SetText(buffer);
            }
        }

        public override void SetText(string text)
        {
            //Ensure the text is within our bounds
            if (text.Length > MAX_BUFFER)
            {
                text = text.Substring(0, MAX_BUFFER);
            }

            buffer = text;
            base.SetText(text);
        }
    }
}
