using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using Minalear;
using Minalear.UI;
using Minalear.UI.Controls;
using DongLife.Code;
using OpenTK.Input;

namespace DongLife.Scenes
{
    public class TestScene : Scene
    {
        private GeoRenderer renderer;
        private Background background;
        
        private GeoPanel panel;
        private GeoInput input;

        private Texture2D maleTexture, femaleTexture;

        public TestScene() : base("TestScene")
        {
            background = new Background(@"Textures/Backgrounds/hospital_room.png");
            background.DrawOrder = 1f;
        }

        public override void LoadContent(ContentManager content)
        {
            renderer = new GeoRenderer(
                content.LoadShader(@"Shaders/geovert.glsl", @"Shaders/geofrag.glsl"),
                Manager.Game.Window.Width, Manager.Game.Window.Height);

            maleTexture = content.LoadTexture2D(@"Textures/Actors/player_male.png");
            femaleTexture = content.LoadTexture2D(@"Textures/Actors/player_female.png");

            //UI Setup
            panel = new GeoPanel(renderer);
            panel.Position = new Vector2(10, 10);
            panel.Size = new Vector2(1260, 700);

            input = new GeoInput(renderer, 640, 48);
            input.Position = new Vector2(600, 50);

            AddChild(background);
            AddChild(panel);
            AddChild(input);

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            maleTexture.Dispose();
            femaleTexture.Dispose();

            base.UnloadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(maleTexture, new RectangleF(75, 100, 400, 548), Color4.White);
        }
    }

    public class GeoButton : Control
    {
        private GeoRenderer renderer;
        private ButtonModes currentMode;

        public GeoButton(GeoRenderer renderer)
        {
            this.renderer = renderer;
            this.DrawOrder = 0.1f;
            this.currentMode = ButtonModes.Normal;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            renderer.Begin();
            if (currentMode == ButtonModes.Normal)
                renderer.FillRect(Position, Size, Color4.Blue);
            else if (currentMode == ButtonModes.Hover)
                renderer.FillRect(Position, Size, Color4.Green);
            else
                renderer.FillRect(Position, Size, Color4.Red);

            renderer.DrawRect(Position, Size, Color4.White);
            renderer.FillCircle(Position + Size / 2, 10f, 4, Color4.White);
            renderer.End();
        }
        public override void Update(GameTime gameTime)
        {
            
        }

        public override void OnMouseEnter()
        {
            currentMode = ButtonModes.Hover;
        }
        public override void OnMouseLeave()
        {
            currentMode = ButtonModes.Normal;
        }
        public override void OnMouseDown(MouseButtonEventArgs e)
        {
            currentMode = ButtonModes.Pressed;
        }
        public override void OnMouseUp(MouseButtonEventArgs e)
        {
            currentMode = ButtonModes.Hover;
        }

        private enum ButtonModes { Normal, Hover, Pressed }
    }
    public class GeoPanel : Control
    {
        private GeoRenderer renderer;

        public GeoPanel(GeoRenderer renderer)
        {
            this.renderer = renderer;
            this.DrawOrder = 0.9f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            renderer.Begin();
            renderer.FillRect(Position, Size, new Color4(0.2f, 0.2f, 0.2f, 0.8f));
            renderer.DrawRect(new Vector2(PosX + 1, PosY + 1), new Vector2(Width - 2, Height - 2), Color4.White);
            renderer.End();
        }
    }
    public class GeoInput : Control
    {
        private GeoRenderer renderer;

        private Bitmap bitmap;
        private Graphics graphics;
        private Texture2D renderTexture;
        private Font font;

        private string buffer = String.Empty;
        private const int MAX_BUFFER = 32;

        public GeoInput(GeoRenderer renderer, int width, int height)
        {
            this.renderer = renderer;
            Width = width;
            Height = height;

            DrawOrder = 0.1f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(renderTexture, Bounds, Color4.White);

            base.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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

            buffer = String.Empty;

            base.UnloadContent();
        }

        public override void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (buffer.Length < MAX_BUFFER)
            {
                buffer += e.KeyChar;
                updateTexture();
            }

            base.OnKeyPress(sender, e);
        }
        public override void OnKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.BackSpace && buffer.Length > 0)
            {
                buffer = buffer.Substring(0, buffer.Length - 1);
                updateTexture();
            }

            base.OnKeyUp(sender, e);
        }

        private void initTexture()
        {
            graphics.Clear(Color.Transparent);
            graphics.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), 
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            renderTexture = new Texture2D(bitmap.Width, bitmap.Height, data.Scan0);

            bitmap.UnlockBits(data);
        }
        private void updateTexture()
        {
            graphics.Clear(Color.Transparent);
            graphics.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            graphics.DrawString(buffer, font, Brushes.White, 0f, 0f);

            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            renderTexture.UpdateTexture(data.Scan0);

            bitmap.UnlockBits(data);
        }
    }
}
