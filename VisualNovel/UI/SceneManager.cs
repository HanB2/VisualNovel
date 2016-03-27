using System;
using System.Collections.Generic;
using OpenTK.Input;

namespace Minalear.UI
{
    public class SceneManager : IDrawable
    {
        private Game game;
        private Dictionary<string, Scene> scenes;
        private Scene currentScene;

        public SceneManager(Game game)
        {
            this.game = game;
            scenes = new Dictionary<string, Scene>();

            //Event Registers
            game.Window.MouseMove += Window_MouseMove;
            game.Window.MouseDown += Window_MouseDown;
            game.Window.MouseUp += Window_MouseUp;
            game.Window.MouseEnter += Window_MouseEnter;
            game.Window.MouseLeave += Window_MouseLeave;
            game.Window.MouseWheel += Window_MouseWheel;

            game.Window.KeyDown += Window_KeyDown;
            game.Window.KeyUp += Window_KeyUp;
            game.Window.KeyPress += Window_KeyPress;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScene.Draw(spriteBatch);
        }
        public void Update(GameTime gameTime)
        {
            currentScene.Update(gameTime);
        }

        public void RegisterScene(Scene scene)
        {
            scene.Manager = this;
            scenes.Add(scene.Name, scene);
        }
        public void SetScene(string sceneName)
        {
            currentScene = scenes[sceneName];
            currentScene.LoadContent(game.Content);
            currentScene.OnEnter();
        }
        public void ChangeScene(string sceneName)
        {
            currentScene.OnExit();
            currentScene.UnloadContent();

            SetScene(sceneName);
        }

        #region MouseEvents
        private void Window_MouseMove(object sender, MouseMoveEventArgs e)
        {
            currentScene.OnMouseMove(e);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            currentScene.OnMouseDown(e);
        }
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            currentScene.OnMouseUp(e);
        }
        private void Window_MouseEnter(object sender, EventArgs e)
        {
            currentScene.OnMouseEnter();
        }
        private void Window_MouseLeave(object sender, EventArgs e)
        {
            currentScene.OnMouseLeave();
        }
        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            currentScene.OnMouseWheel(e);
        }
        #endregion
        #region KeyboardEvents
        private void Window_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            currentScene.OnKeyDown(sender, e);
        }
        private void Window_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            currentScene.OnKeyUp(sender, e);
        }
        private void Window_KeyPress(object sender, OpenTK.KeyPressEventArgs e)
        {
            currentScene.OnKeyPress(sender, e);
        }
        #endregion

        public Scene CurrentScene
        {
            get { return this.currentScene; }
            set { this.ChangeScene(value.Name); }
        }
        public Game Game
        {
            get { return this.game; }
        }
    }
}
