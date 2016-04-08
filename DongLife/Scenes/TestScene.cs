using System;
using OpenTK;
using OpenTK.Graphics;
using Minalear;
using Minalear.UI;
using Minalear.UI.Controls;
using DongLife.Code;
using DongLife.Controls;
using OpenTK.Input;

namespace DongLife.Scenes
{
    public class TestScene : Scene
    {
        private GeoRenderer renderer;
        private Background background;

        private Texture2D playerBase;
        private Texture2D[] Hats;
        private Texture2D[] Shirts;
        private Texture2D[] Accessories;

        private GeoButton button;
        private GeoPanel panel;

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

            button = new GeoButton(renderer);
            button.Position = new Vector2(15, 15);
            button.Size = new Vector2(30, 30);

            panel = new GeoPanel(renderer);
            panel.Position = new Vector2(10, 10);
            panel.Size = new Vector2(1260, 700);

            AddChild(background);
            AddChild(button);
            AddChild(panel);

            base.LoadContent(content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
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
}
