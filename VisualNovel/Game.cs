using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Audio;

namespace Minalear
{
    public class Game : IDisposable
    {
        private GameWindow gameWindow;
        private ContentManager content;
        private GameTime gameTime;
        private AudioContext audioContext;
        private Color4 clearColor = Color4.Black;

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
            audioContext = new AudioContext();
        }

        public void Run()
        {
            Initialize();
            LoadContent();

            gameWindow.Run();
        }
        public void Resize()
        {
            GL.ClearColor(clearColor);
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
        public AudioContext AudioContext { get { return this.audioContext; } }
        public Color4 ClearColor { get { return this.clearColor; } set { this.clearColor = value; } }
    }
}
