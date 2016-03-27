using System;
using Minalear;
using Minalear.UI;
using DongLife.Scenes;

namespace DongLife
{
    public class MainGame : Game
    {
        private SpriteBatch spriteBatch;
        private SceneManager sceneManager;

        public MainGame(int width, int height) : base(width, height, "Life with a Massive Dong™")
        {
            Window.Icon = new System.Drawing.Icon("favicon.ico");
        }

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(
                Content.LoadShader(@"Shaders/vert.glsl", @"Shaders/frag.glsl"),
                Window.Width, Window.Height);
            sceneManager = new SceneManager(this);

            //Message Box init
            VNScene.MessageBox = new Controls.MessageBox(
                GameSettings.WindowWidth / 2,
                GameSettings.WindowHeight / 3);
            VNScene.MessageBox.PosX = GameSettings.WindowWidth / 2 - VNScene.MessageBox.Width / 2;
            VNScene.MessageBox.PosY = GameSettings.WindowHeight - VNScene.MessageBox.Height;

            //Scene Creation
            sceneManager.RegisterScene(new TestScene());
            sceneManager.RegisterScene(new RenderScene());
            sceneManager.RegisterScene(new MainMenuScene());

            //Game Scenes
            sceneManager.RegisterScene(new IN_HospitalScene());
            sceneManager.RegisterScene(new IN_Home());

            sceneManager.SetScene("IN_HospitalScene");
        }
        public override void Draw(GameTime gameTime)
        {
            sceneManager.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            sceneManager.Update(gameTime);
        }

        public override void LoadContent()
        {
            VNScene.MessageBox.LoadContent(Content);

            base.LoadContent();
        }
        public override void UnloadContent()
        {
            VNScene.MessageBox.UnloadContent();

            base.UnloadContent();
        }
    }
}
