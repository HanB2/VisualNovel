﻿using Minalear;
using DongLife.Code;
using DongLife.Scenes;
using DongLife.Scenes.EndScenes;
using DongLife.Scenes.GameScenes;

namespace DongLife
{
    public class MainGame : Game
    {
        private SpriteBatch spriteBatch;
        private VNSceneManager sceneManager;

        public MainGame(int width, int height) : base(width, height, "Life with a Massive Dong™")
        {
            Window.Icon = new System.Drawing.Icon("favicon.ico");
        }

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(
                Content.LoadShader(@"Shaders/vert.glsl", @"Shaders/frag.glsl"),
                Window.Width, Window.Height);
            sceneManager = new VNSceneManager(this, spriteBatch);
            //MusicManager.Init(Content);

            //Message Box init
            VNScene.MessageBox = new Controls.MessageBox(
                GameSettings.WindowWidth / 2,
                GameSettings.WindowHeight / 3);
            VNScene.MessageBox.PosX = GameSettings.WindowWidth / 2 - VNScene.MessageBox.Width / 2;
            VNScene.MessageBox.PosY = GameSettings.WindowHeight - VNScene.MessageBox.Height;

            AccessoryManager.Init();
            ActorFactory.Init();

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
            sceneManager.RegisterScene(new SCHL_AlternateOffice());
            sceneManager.RegisterScene(new SCHL_Detention());
            sceneManager.RegisterScene(new SCHL_Date());
            sceneManager.RegisterScene(new SCHL_Alley());
            sceneManager.RegisterScene(new SLAVE_HomeAmbush());
            sceneManager.RegisterScene(new SLAVE_Base());
            sceneManager.RegisterScene(new SCHL_Hallway());
            sceneManager.RegisterScene(new KIJU_Home());
            sceneManager.RegisterScene(new KIJU_HomeArrest());
            sceneManager.RegisterScene(new KIJU_WorldDomination());
            sceneManager.RegisterScene(new CORT_Base());
            sceneManager.RegisterScene(new JAGR_HomeAmbush());
            sceneManager.RegisterScene(new JAGR_Alley());
            sceneManager.RegisterScene(new JAGR_JewelryStore());
            sceneManager.RegisterScene(new CMP_Base());
            sceneManager.RegisterScene(new CMP_Matrix());
            sceneManager.RegisterScene(new CMP_Nic());
            sceneManager.RegisterScene(new ROE_Weed());

            //Good Endings
            sceneManager.RegisterScene(new GEND_TvStar_Shia());
            sceneManager.RegisterScene(new GEND_DongMolePeople());
            sceneManager.RegisterScene(new GEND_HeadHoncho());
            sceneManager.RegisterScene(new GEND_FinalEnding());
            sceneManager.RegisterScene(new GEND_TimeParadox());
            sceneManager.RegisterScene(new GEND_GrowOldShia());
            sceneManager.RegisterScene(new GEND_KaijuSlave());
            sceneManager.RegisterScene(new GEND_KingKaiju());
            sceneManager.RegisterScene(new GEND_EthnicCleansing());
            sceneManager.RegisterScene(new GEND_Nic());
            sceneManager.RegisterScene(new GEND_Weed());

            //Bad Endings
            sceneManager.RegisterScene(new BEND_EatenAlive_Shia());
            sceneManager.RegisterScene(new BEND_DeadInBasement());
            sceneManager.RegisterScene(new BEND_SandCoffin());
            sceneManager.RegisterScene(new BEND_Suicide());
            sceneManager.RegisterScene(new BEND_SlaveDeath());
            sceneManager.RegisterScene(new BEND_SlaveSleepDeath());
            sceneManager.RegisterScene(new BEND_SlaveDieByGuards());
            sceneManager.RegisterScene(new BEND_SlaveGunDeath());
            sceneManager.RegisterScene(new BEND_SlaveWorkToDeath());
            sceneManager.RegisterScene(new BEND_DetentionDeath());
            sceneManager.RegisterScene(new BEND_DateDeath());
            sceneManager.RegisterScene(new BEND_KaijuDeath());
            sceneManager.RegisterScene(new BEND_PsychiatricWard());
            sceneManager.RegisterScene(new BEND_Prison());
            sceneManager.RegisterScene(new BEND_JaegerDeath());
            sceneManager.RegisterScene(new BEND_Electrocution());
            sceneManager.RegisterScene(new BEND_DrownedExplicit());
            sceneManager.RegisterScene(new BEND_Roofie());
            sceneManager.RegisterScene(new BEND_Weed());
            sceneManager.RegisterScene(new BEND_Overdose());

            //Music Track Registration
            /*MusicManager.RegisterSong("In_Pursuit", @"Audio/In_Pursuit.wav");
            MusicManager.RegisterSong("Necropolis", @"Audio/Necropolis.wav");
            MusicManager.RegisterSong("TrevorSux", @"Audio/TrevorSux.wav");
            MusicManager.SetVolume(0f);*/

            //Set first scene
            sceneManager.SetScene("MainMenuScene");
        }
        public override void Draw(GameTime gameTime)
        {
            sceneManager.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            sceneManager.Update(gameTime);
            //MusicManager.Update(gameTime);
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
