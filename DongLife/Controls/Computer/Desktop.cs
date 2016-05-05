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

        private Window pornWindow, homeworkWindow, newsWindow;
        private VNScene parent;

        public Desktop(VNScene parent)
        {
            this.parent = parent;

            this.DrawOrder = 0.9f;
            this.Bounds = new RectangleF(0f, 0f, 1280f, 720f);

            pornWindow = new Window(@"Textures/Misc/window_porn.png");
            homeworkWindow = new Window(@"Textures/Misc/window_homework.png");
            newsWindow = new Window(@"Textures/Misc/window_news.png");

            pornWindow.CloseButton.SingleClick += pornWindowClose;
            homeworkWindow.CloseButton.SingleClick += homeworkWindowClose;
            newsWindow.CloseButton.SingleClick += newsWindowClose;

            //Disable and Hide all windows
            pornWindow.Enabled = false;
            pornWindow.Visible = false;
            homeworkWindow.Enabled = false;
            homeworkWindow.Visible = false;
            newsWindow.Enabled = false;
            newsWindow.Visible = false;

            //Window Icons
            Icon homework_essay01 = new Icon(466, 131, 134, 186);
            homework_essay01.SingleClick += (sender, e) =>
            {

            };
            Icon homework_essay02 = new Icon(609, 131, 134, 186);
            homework_essay02.SingleClick += (sender, e) =>
            {

            };
            Icon homework_essay03 = new Icon(752, 131, 134, 186);
            homework_essay03.SingleClick += (sender, e) =>
            {

            };

            homeworkWindow.AddChild(homework_essay01);
            homeworkWindow.AddChild(homework_essay02);
            homeworkWindow.AddChild(homework_essay03);

            Icon porn_link01 = new Icon(457, 132, 105, 155);
            porn_link01.SingleClick += (sender, e) =>
            {
                parent.Sequences.SetStage(12);
                parent.Sequences.ExecuteSequence(parent);
                Enabled = false;
            };
            Icon porn_link02 = new Icon(567, 132, 105, 155);
            porn_link02.SingleClick += (sender, e) =>
            {
                parent.Sequences.SetStage(14);
                parent.Sequences.ExecuteSequence(parent);
                Enabled = false;
            };
            Icon porn_link03 = new Icon(676, 132, 105, 155);
            porn_link03.SingleClick += (sender, e) =>
            {
                parent.Sequences.SetStage(16);
                parent.Sequences.ExecuteSequence(parent);
                Enabled = false;
            };
            Icon porn_link04 = new Icon(786, 132, 105, 155);
            porn_link04.SingleClick += (sender, e) =>
            {
                parent.Sequences.SetStage(18);
                parent.Sequences.ExecuteSequence(parent);
                Enabled = false;
            };

            pornWindow.AddChild(porn_link01);
            pornWindow.AddChild(porn_link02);
            pornWindow.AddChild(porn_link03);
            pornWindow.AddChild(porn_link04);

            pornIcon =      new Icon(new RectangleF(34f,  22f, 66f, 68f));
            homeworkIcon =  new Icon(new RectangleF(34f, 130f, 66f, 62f));
            newsIcon =      new Icon(new RectangleF(34f, 210f, 66f, 65f));
            settingsIcon =  new Icon(new RectangleF(34f, 572f, 66f, 68f));
            quitButton =    new Icon(new RectangleF(4f, 672f, 108f, 44f));

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

            AddChild(pornWindow);
            AddChild(homeworkWindow);
            AddChild(newsWindow);
        }

        public void ResetDesktop()
        {
            Enabled = true;

            pornWindow.Enabled = false;
            pornWindow.Visible = false;
            homeworkWindow.Enabled = false;
            homeworkWindow.Visible = false;
            newsWindow.Enabled = false;
            newsWindow.Visible = false;
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
        
        private void QuitButton_OnIconClick(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            VNScene.MessageBox.CurrentTheme = MessageBox.Themes.Normal;
            (Parent as Scene).Manager.ChangeScene("BASE_Home");
        }

        //Close Buttons
        private void pornWindowClose(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            pornWindow.Visible = false;
            pornWindow.Enabled = false;

            parent.Sequences.SetStage(0);
            parent.Sequences.ExecuteSequence(parent);
        }
        private void homeworkWindowClose(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            homeworkWindow.Visible = false;
            homeworkWindow.Enabled = false;

            parent.Sequences.SetStage(0);
            parent.Sequences.ExecuteSequence(parent);
        }
        private void newsWindowClose(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            newsWindow.Visible = false;
            newsWindow.Enabled = false;

            parent.Sequences.SetStage(0);
            parent.Sequences.ExecuteSequence(parent);
        }

        //Icons
        private void Porn_OnIconClick(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            pornWindow.Visible = true;
            pornWindow.Enabled = true;

            newsWindow.Visible = false;
            newsWindow.Enabled = false;
            homeworkWindow.Visible = false;
            homeworkWindow.Enabled = false;

            parent.Sequences.SetStage(10);
            parent.Sequences.ExecuteSequence(parent);
        }
        private void Homework_OnIconClick(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            homeworkWindow.Visible = true;
            homeworkWindow.Enabled = true;

            pornWindow.Visible = false;
            pornWindow.Enabled = false;
            newsWindow.Visible = false;
            newsWindow.Enabled = false;

            parent.Sequences.SetStage(20);
            parent.Sequences.ExecuteSequence(parent);
        }
        private void News_OnIconClick(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            newsWindow.Visible = true;
            newsWindow.Enabled = true;

            homeworkWindow.Visible = false;
            homeworkWindow.Enabled = false;
            pornWindow.Visible = false;
            pornWindow.Enabled = false;

            parent.Sequences.SetStage(30);
            parent.Sequences.ExecuteSequence(parent);
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

            VNScene.MessageBox.SetText("Oooh... pretty wallpaper.");
        }
    }
}
