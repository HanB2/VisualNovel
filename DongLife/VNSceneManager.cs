using System;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Minalear;
using Minalear.UI;

namespace DongLife
{
    public class VNSceneManager : SceneManager
    {
        private SpriteBatch spriteBatch;

        //Base scene rendering, destination scene rendering, transition rendering
        private int fboBase, fboDest, fboTran;
        private int texBase, texDest, texTran;

        private bool transitioning = false;
        private float transitionX = 0f;

        public VNSceneManager(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
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
                spriteBatch.DirectDraw(texDest, new RectangleF(0, 0, Game.Window.Width, Game.Window.Height), Color4.White, RenderFlags.Blur | RenderFlags.Desaturate);
                spriteBatch.DirectDraw(texBase, new RectangleF(transitionX, 0, Game.Window.Width, Game.Window.Height), Color4.White, RenderFlags.Blur | RenderFlags.Desaturate);
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (!transitioning)
                base.Update(gameTime);
            else
            {
                transitionX -= 500f * (float)gameTime.ElapsedTime.TotalSeconds;
                if (transitionX < -Game.Window.Width)
                    transitioning = false;
            }
        }
        public override void SetScene(string sceneName)
        {
            base.SetScene(sceneName);
        }
        public override void ChangeScene(string sceneName)
        {
            transitioning = true;
            transitionX = 0f;

            //Draw current scene to base fbo
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboBase);
            base.Draw(spriteBatch);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            //Change scene
            base.ChangeScene(sceneName);

            //Render new scene to dest fbo
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboDest);
            base.Draw(spriteBatch);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        private void initFBO()
        {
            //FBO
            fboBase = GL.GenFramebuffer();
            fboDest = GL.GenFramebuffer();

            //Textures
            texBase = GL.GenTexture();
            texDest = GL.GenTexture();

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
        }
    }
}
