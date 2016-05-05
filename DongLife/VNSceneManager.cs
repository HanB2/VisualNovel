using System;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Minalear;
using Minalear.UI;
using DongLife.Animations;
using OpenTK.Input;

namespace DongLife
{
    public class VNSceneManager : SceneManager
    {
        //SpriteBatch used to render outside of Draw()
        private SpriteBatch spriteBatch;
        private TransitionRenderer renderer;
        private Transition transition;

        //Base scene rendering, destination scene rendering, transition rendering
        private int fboBase, fboDest, fboTran;
        private int texBase, texDest, texTran;

        private bool transitioning = false;
        public bool Transitioning { get { return transitioning; } }

        public VNSceneManager(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;

            renderer = new TransitionRenderer(
                Game.Content.LoadShader(@"Shaders/tvert.glsl", @"Shaders/tfrag.glsl"), 
                Game.Window.Width, Game.Window.Height);
            GameManager.Renderer = new GeoRenderer(
                Game.Content.LoadShader(@"Shaders/geovert.glsl", @"Shaders/geofrag.glsl"),
                Game.Window.Width, Game.Window.Height);

            transition = new Transition(16f);
            transition.LoadContent(Game.Content);

            initFBO();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!transitioning)
            {
                //Draw the scene
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboBase);
                GL.Viewport(0, 0, 1280, 720);
                GL.Clear(ClearBufferMask.ColorBufferBit);

                base.Draw(spriteBatch);

                //Draw the FBO
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
                GL.Viewport(0, 0, Game.Window.Width, Game.Window.Height);
                GL.Clear(ClearBufferMask.ColorBufferBit);

                spriteBatch.DirectDraw(texBase, new RectangleF(0, 0, Game.Window.Width, Game.Window.Height), Color4.White);
            }
            else
            {
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboTran);
                GL.Viewport(0, 0, Game.Window.Width, Game.Window.Height);
                GL.Clear(ClearBufferMask.ColorBufferBit);

                transition.Draw(spriteBatch);

                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

                renderer.DrawTransition(texBase, texDest, texTran);
                //spriteBatch.DirectDraw(texDest, new RectangleF(0, 0, Game.Window.Width, Game.Window.Height), Color4.White, RenderFlags.Blur | RenderFlags.Desaturate);
                //spriteBatch.DirectDraw(texBase, new RectangleF(0, 0, Game.Window.Width, Game.Window.Height), Color4.White, RenderFlags.Blur | RenderFlags.Desaturate);
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (!transitioning)
                base.Update(gameTime);
            else
            {
                transition.Update(gameTime);
                transitioning = !transition.Done();
            }
        }
        public override void SetScene(string sceneName)
        {
            base.SetScene(sceneName);
        }
        public override void ChangeScene(string sceneName)
        {
            transitioning = true;
            transition.BeginTransition();

            //Draw current scene to base fbo
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboBase);
            base.Draw(spriteBatch);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            //Really gross hack to fix the player position bug
            Code.ActorFactory.ResetPlayerPosition();

            //Change scene
            base.ChangeScene(sceneName);

            //Render new scene to dest fbo
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboDest);
            base.Draw(spriteBatch);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        protected override void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (transitioning)
            {
                transitioning = false;
            }
            else
            {
                base.Window_MouseUp(sender, e);
            }
        }

        private void initFBO()
        {
            //FBO
            fboBase = GL.GenFramebuffer();
            fboDest = GL.GenFramebuffer();
            fboTran = GL.GenFramebuffer();

            //Textures
            texBase = GL.GenTexture();
            texDest = GL.GenTexture();
            texTran = GL.GenTexture();

            //Base FBO
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboBase);
            GL.BindTexture(TextureTarget.Texture2D, texBase);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, Game.Window.Width, Game.Window.Height, 
                0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, texBase, 0);
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);

            //Dest FBO
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboDest);
            GL.BindTexture(TextureTarget.Texture2D, texDest);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, Game.Window.Width, Game.Window.Height,
                0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, texDest, 0);
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);

            //Transition FBO
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboTran);
            GL.BindTexture(TextureTarget.Texture2D, texTran);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, Game.Window.Width, Game.Window.Height,
                0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, texTran, 0);
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
        }
    }
}
