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
        private Texture2D wallpaper;
        private Texture2D desktop;

        private Icon porn, homework, news, settings;
        private Icon quitButton;

        public Desktop()
        {
            this.DrawOrder = 0.9f;
            this.Bounds = new RectangleF(0f, 0f, 1280f, 720f);

            porn =      new Icon(new RectangleF(34f,  22f, 66f, 68f));
            homework =  new Icon(new RectangleF(34f, 130f, 66f, 62f));
            news =      new Icon(new RectangleF(34f, 210f, 66f, 65f));
            settings =  new Icon(new RectangleF(34f, 572f, 66f, 68f));
            quitButton = new Icon(new RectangleF(4f, 672f, 108f, 44f));

            porn.DoubleClick += Porn_OnIconClick;
            homework.DoubleClick += Homework_OnIconClick;
            news.DoubleClick += News_OnIconClick;
            settings.DoubleClick += Settings_OnIconClick;
            quitButton.SingleClick += QuitButton_OnIconClick;

            AddChild(porn);
            AddChild(homework);
            AddChild(news);
            AddChild(settings);
            AddChild(quitButton);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.wallpaper, new RectangleF(0f, 0f, 1280f, 720f), Color4.White);
            spriteBatch.Draw(this.desktop, new RectangleF(0f, 0f, 1280f, 720f), Color4.White);

            base.Draw(spriteBatch);
        }
        public override void LoadContent(ContentManager content)
        {
            wallpaper = content.LoadTexture2D(@"Textures/Wallpapers/wallpaper_01.png");
            desktop = content.LoadTexture2D(@"Textures/Misc/desktop.png");

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            wallpaper.Dispose();
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

        }
        private void QuitButton_OnIconClick(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            VNScene.MessageBox.CurrentTheme = MessageBox.Themes.Normal;
            (Parent as Scene).Manager.ChangeScene("BASE_Home");
        }
    }
}
