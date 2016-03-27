using System;
using Minalear;
using UIPrototype.Scenes;

namespace UIPrototype
{
    public class MainGame : Game
    {
        private SpriteBatch spriteBatch;
        private UISceneManager sceneManager;

        public MainGame() : base(800, 450, "UI Prototype") { }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(
                Content.LoadShader(@"Shaders/vert.glsl", @"Shaders/frag.glsl"), 
                Window.Width, Window.Height);

            sceneManager = new UISceneManager(this, Content);
            sceneManager.RegisterScene(new Scene01());
            sceneManager.RegisterScene(new Scene02());

            sceneManager.SetScene("Scene01");
        }

        public override void Draw(GameTime gameTime)
        {
            sceneManager.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            sceneManager.Update(gameTime);
        }
    }
}
