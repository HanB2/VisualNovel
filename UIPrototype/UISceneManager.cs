using System;
using System.Collections.Generic;
using OpenTK.Input;
using Minalear;
using UIPrototype.Controls;

namespace UIPrototype
{
    public class UISceneManager : IDrawable
    {
        private Game game;
        private Dictionary<string, UIScene> scenes;
        private UIScene currentScene;
        private ContentManager content;

        public UISceneManager(Game game, ContentManager content)
        {
            this.game = game;
            this.content = content;
            scenes = new Dictionary<string, UIScene>();

            //Mouse Events
            game.Window.MouseMove += Window_MouseMove;
            game.Window.MouseDown += Window_MouseDown;
            game.Window.MouseUp += Window_MouseUp;
            game.Window.MouseEnter += Window_MouseEnter;
            game.Window.MouseLeave += Window_MouseLeave;
            game.Window.MouseWheel += Window_MouseWheel;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScene.Draw(spriteBatch);
        }
        public void Update(GameTime gameTime)
        {
            currentScene.Update(gameTime);
        }

        public void RegisterScene(UIScene scene)
        {
            scene.Manager = this;
            scenes.Add(scene.Name, scene);
        }

        public void SetScene(string sceneName)
        {
            currentScene = scenes[sceneName];
            currentScene.OnEnter();

            currentScene.LoadContent(content);
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

        public UIScene CurrentScene
        {
            get { return this.currentScene; }
            set { this.ChangeScene(value.Name); }
        }
    }
}
