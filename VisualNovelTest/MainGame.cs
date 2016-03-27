using System;
using System.Drawing;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Minalear;

namespace VisualNovelTest
{
    public class MainGame : Game
    {
        private SpriteBatch spriteBatch;
        private Texture2D background;

        private TextBox textBox;
        private Character char01, char02;

        public MainGame()
            : base(800, 450)
        { }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(
                Content.LoadShader(@"Shaders/vert.glsl", @"Shaders/frag.glsl"),
                Window.Width, Window.Height);

            background = Texture2D.LoadFromSource(@"Textures/background.bmp");

            char01 = new Character(Texture2D.LoadFromSource(@"Textures/man_01.png"));
            char01.Position = new Vector2(45f, 100f);
            char01.Scale = 0.5f;
            char01.SetFocus(true);

            char02 = new Character(Texture2D.LoadFromSource(@"Textures/man_02.png"));
            char02.Position = new Vector2(300f, 40f);
            char02.Scale = 1f;
            char02.SetFocus(false);

            textBox = new TextBox();
            textBox.SetText("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc bibendum accumsan justo eu euismod. Nunc quis ante bibendum dui tempor porta et a lectus.");
        }
        public override void UnloadContent()
        {
            background.Dispose();
        }

        public override void Draw(GameTime gameTime)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            spriteBatch.Draw(background, Vector2.Zero, Color4.White, 0f, Vector2.Zero, 1f, RenderFlags.Blur | RenderFlags.Desaturate);
            char01.Draw(spriteBatch);
            char02.Draw(spriteBatch);

            textBox.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            char01.Update(gameTime);
            char02.Update(gameTime);

            textBox.Update(gameTime);

            if (char02.HasFocus && Keyboard.GetState()[Key.Left])
            {
                char01.SetFocus(true);
                char02.SetFocus(false);

                textBox.SetText("Hey man, would you like some dank weed?  I only sell the highest quality of weed!");
            }
            else if (char01.HasFocus && Keyboard.GetState()[Key.Right])
            {
                char01.SetFocus(false);
                char02.SetFocus(true);

                textBox.SetText("Neh im nut a fgt lik u.  #vapeLyfe");
            }
        }
    }
}
