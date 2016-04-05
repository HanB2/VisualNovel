﻿using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Minalear.UI;

namespace Minalear
{
    public class Game : IDisposable
    {
        private GameWindow gameWindow;
        private ContentManager content;
        private GameTime gameTime;

        public Game() : this(800, 450) { }
        public Game(int width, int height) : this(width, height, "VisualNovel") { }
        public Game(int width, int height, string title)
        {
            gameWindow = new GameWindow(width, height, new GraphicsMode(32, 24, 8, 4), title, GameWindowFlags.FixedWindow);
            gameWindow.UpdateFrame += (sender, e) => updateFrame(e);
            gameWindow.RenderFrame += (sender, e) => renderFrame(e);
            gameWindow.Resize += (sender, e) => Resize();

            //Enable Alpha blending
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            gameTime = new GameTime();
            content = new ContentManager();
        }

        public void Run()
        {
            Initialize();
            LoadContent();

            gameWindow.Run();
        }
        public void Resize()
        {
            GL.ClearColor(Color.Black);
            GL.Viewport(0, 0, gameWindow.Width, gameWindow.Height);
        }

        public virtual void Initialize() { }
        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime) { }
        public virtual void Dispose()
        {
            UnloadContent();
        }

        protected virtual void updateFrame(FrameEventArgs e)
        {
            if (Window.Focused)
            {
                gameTime.ElapsedTime = TimeSpan.FromSeconds(e.Time);
                gameTime.TotalTime.Add(TimeSpan.FromSeconds(e.Time));

                Update(gameTime);
            }
        }
        protected virtual void renderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Draw(gameTime);
            gameWindow.SwapBuffers();
        }

        public GameWindow Window { get { return this.gameWindow; } }
        public ContentManager Content { get { return this.content; } }
    }
}
