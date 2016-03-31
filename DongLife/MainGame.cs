﻿using System;
using Minalear;
using Minalear.UI;
using DongLife.Scenes;
using DongLife.Scenes.EndScenes;
using DongLife.Scenes.GameScenes;

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

            GameManager.Init();

            //Scene Creation
            sceneManager.RegisterScene(new MainMenuScene());

            //Game Scenes
            sceneManager.RegisterScene(new IN_HospitalScene());
            sceneManager.RegisterScene(new IN_Home());
            sceneManager.RegisterScene(new BASE_Home());
            sceneManager.RegisterScene(new MOM_Base());
            sceneManager.RegisterScene(new MOM_Seduction());
            sceneManager.RegisterScene(new MOM_Cambodia());
            sceneManager.RegisterScene(new MOM_Basement());
            sceneManager.RegisterScene(new SCHL_Base());
            sceneManager.RegisterScene(new SCHL_SchoolRoom());
            sceneManager.RegisterScene(new SCHL_PrincipalOffice());
            sceneManager.RegisterScene(new SCHL_Alley());

            sceneManager.RegisterScene(new GEND_TvStar_Shia());
            sceneManager.RegisterScene(new GEND_DongMolePeople());
            sceneManager.RegisterScene(new BEND_EatenAlive_Shia());
            sceneManager.RegisterScene(new BEND_DeadInBasement());
            sceneManager.RegisterScene(new BEND_SandCoffin());
            sceneManager.RegisterScene(new BEND_Suicide());

            sceneManager.SetScene("SCHL_Base");
            
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
