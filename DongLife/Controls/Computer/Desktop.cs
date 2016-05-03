using System;
using System.Drawing;
using Minalear;
using Minalear.UI;
using Minalear.UI.Controls;
using OpenTK.Graphics;
using DongLife.Scenes;

namespace DongLife.Controls.Computer
{
    public class Desktop : Control
    {
        private Texture2D[] wallpapers;
        private int currentWallpaperID = 0;
        private Texture2D desktop;

        private Icon pornIcon, homeworkIcon, newsIcon, settingsIcon;
        private Icon quitButton;

        private Window porn, homework, news;

        public Desktop()
        {
            this.DrawOrder = 0.9f;
            this.Bounds = new RectangleF(0f, 0f, 1280f, 720f);

            pornIcon =      new Icon(new RectangleF(34f,  22f, 66f, 68f));
            homeworkIcon =  new Icon(new RectangleF(34f, 130f, 66f, 62f));
            newsIcon =      new Icon(new RectangleF(34f, 210f, 66f, 65f));
            settingsIcon =  new Icon(new RectangleF(34f, 572f, 66f, 68f));
            quitButton = new Icon(new RectangleF(4f, 672f, 108f, 44f));

            pornIcon.DoubleClick += Porn_OnIconClick;
            homeworkIcon.DoubleClick += Homework_OnIconClick;
            newsIcon.DoubleClick += News_OnIconClick;
            settingsIcon.DoubleClick += Settings_OnIconClick;
            quitButton.SingleClick += QuitButton_OnIconClick;

            AddChild(pornIcon);
            AddChild(homeworkIcon);
            AddChild(newsIcon);
            AddChild(settingsIcon);
            AddChild(quitButton);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.wallpapers[currentWallpaperID], new RectangleF(0f, 0f, 1280f, 720f), Color4.White);
            spriteBatch.Draw(this.desktop, new RectangleF(0f, 0f, 1280f, 720f), Color4.White);

            base.Draw(spriteBatch);
        }
        public override void LoadContent(ContentManager content)
        {
            string[] wallpaperPaths = System.IO.Directory.GetFiles(@"Content/Textures/Wallpapers/", "*.png");
            this.wallpapers = new Texture2D[wallpaperPaths.Length];

            for (int i = 0; i < wallpaperPaths.Length; i++)
            {
                wallpapers[i] = content.LoadTexture2D(wallpaperPaths[i].Replace("Content/", string.Empty));
            }
            
            desktop = content.LoadTexture2D(@"Textures/Misc/desktop.png");

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            for (int i = 0; i < wallpapers.Length; i++)
                wallpapers[i].Dispose();
            desktop.Dispose();

            base.UnloadContent();
        }

        private void Porn_OnIconClick(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            this.Visible = false;
        }
        private void Homework_OnIconClick(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {

        }
        private void News_OnIconClick(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {

        }
        private void Settings_OnIconClick(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            if (e.Button == OpenTK.Input.MouseButton.Left)
                currentWallpaperID++;
            else if (e.Button == OpenTK.Input.MouseButton.Right)
                currentWallpaperID--;

            if (currentWallpaperID < 0)
                currentWallpaperID = wallpapers.Length - 1;
            else if (currentWallpaperID >= wallpapers.Length)
                currentWallpaperID = 0;
        }
        private void QuitButton_OnIconClick(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            VNScene.MessageBox.CurrentTheme = MessageBox.Themes.Normal;
            (Parent as Scene).Manager.ChangeScene("BASE_Home");
        }
    }
}
