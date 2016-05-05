using System;
using Minalear;
using Minalear.UI;
using Minalear.UI.Controls;
using System.Drawing;
using OpenTK.Input;

namespace DongLife.Controls.Computer
{
    public class Window : Control
    {
        private Icon closeButton;
        private string texturePath;
        private Texture2D texture;

        public Window(string texturePath)
        {
            this.Bounds = new RectangleF(0, 0, 1280, 720);
            closeButton = new Icon(new RectangleF(875, 30, 33, 24));
            this.texturePath = texturePath;

            AddChild(closeButton);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new OpenTK.Vector2(438, 25), OpenTK.Graphics.Color4.White);

            base.Draw(spriteBatch);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.LoadTexture2D(texturePath);

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            texture.Dispose();

            base.UnloadContent();
        }

        public void RegisterIcon(Icon icon)
        {
            AddChild(icon);
        }

        public Icon CloseButton
        {
            get { return this.closeButton; }
        }
    }
}
