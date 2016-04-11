using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Minalear
{
    public class SpriteBatch : IDisposable
    {
        private int vao;
        private Shader shader;

        //Uniform Locations
        private int modelLoc;
        private int projLoc;
        private int blurLoc;
        private int desaturateLoc;
        private int colorLoc;
        private int resolutionLoc;

        public RenderFlags Flags = RenderFlags.None;

        public SpriteBatch(Shader shader, int renderWidth, int renderHeight)
        {
            this.shader = shader;
            
            float[] vertices = new float[] {
                0f, 0f,     //0f, 0f,
                0f, 1f,     //0f, 1f,
                1f, 0f,     //1f, 0f,

                1f, 0f,     //1f, 0f,
                0f, 1f,     //0f, 1f,
                1f, 1f,     //1f, 1f,
            };

            vao = GL.GenVertexArray();
            int vbo = GL.GenBuffer();

            GL.BindVertexArray(vao);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * sizeof(float)), vertices, BufferUsageHint.StaticDraw);

            shader.UseProgram();

            //Attributes
            int posAttrib = GL.GetAttribLocation(shader.ID, "pos");
            GL.EnableVertexAttribArray(posAttrib);
            GL.VertexAttribPointer(posAttrib, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);

            //int texAttrib = GL.GetAttribLocation(shader.ID, "texCoords");
            //GL.EnableVertexAttribArray(texAttrib);
            //GL.VertexAttribPointer(texAttrib, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));

            //Locations
            modelLoc = GL.GetUniformLocation(shader.ID, "model");
            projLoc = GL.GetUniformLocation(shader.ID, "proj");
            blurLoc = GL.GetUniformLocation(shader.ID, "blurImage");
            desaturateLoc = GL.GetUniformLocation(shader.ID, "desaturateImage");
            colorLoc = GL.GetUniformLocation(shader.ID, "drawColor");
            resolutionLoc = GL.GetUniformLocation(shader.ID, "resolution");

            //Projection Matrix
            Matrix4 projMat4 = Matrix4.CreateOrthographicOffCenter(0f, renderWidth, renderHeight, 0f, -1f, 1f);
            GL.UniformMatrix4(projLoc, false, ref projMat4);
        }
        
        //Position/Scale
        public void Draw(Texture2D texture, Vector2 position, Color4 color)
        {
            this.Draw(texture, position, color, 0f, Vector2.Zero, Vector2.One, RenderFlags.None);
        }
        public void Draw(Texture2D texture, Vector2 position, Color4 color, float rotation, Vector2 origin)
        {
            this.Draw(texture, position, color, rotation, origin, Vector2.One, RenderFlags.None);
        }
        public void Draw(Texture2D texture, Vector2 position, Color4 color, float rotation, Vector2 origin, float scale)
        {
            this.Draw(texture, position, color, rotation, origin, scale, RenderFlags.None);
        }
        public void Draw(Texture2D texture, Vector2 position, Color4 color, float rotation, Vector2 origin, Vector2 scale)
        {
            this.Draw(texture, position, color, rotation, origin, scale, RenderFlags.None);
        }
        public void Draw(Texture2D texture, Vector2 position, Color4 color, float rotation, Vector2 origin, float scale, RenderFlags renderFlags)
        {
            this.Draw(texture, position, color, rotation, origin, new Vector2(scale, scale), renderFlags);
        }
        public void Draw(Texture2D texture, Vector2 position, Color4 color, float rotation, Vector2 origin, Vector2 scale, RenderFlags renderFlags)
        {
            //origin *= scale;
            origin = new Vector2(texture.Width * scale.X * origin.X, texture.Height * scale.Y * origin.Y);

            shader.UseProgram();
            setUniforms(position, color, rotation, origin, scale * new Vector2(texture.Width, texture.Height), renderFlags);

            GL.ActiveTexture(TextureUnit.Texture0);
            texture.Bind();

            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.BindVertexArray(0);
        }

        //Rectangle
        public void Draw(Texture2D texture, RectangleF bounds, Color4 color)
        {
            this.Draw(texture, bounds, color, 0f, Vector2.Zero, RenderFlags.None);
        }
        public void Draw(Texture2D texture, RectangleF bounds, Color4 color, float rotation, Vector2 origin)
        {
            this.Draw(texture, bounds, color, rotation, origin, RenderFlags.None);
        }
        public void Draw(Texture2D texture, RectangleF bounds, Color4 color, float rotation, Vector2 origin, RenderFlags renderFlags)
        {
            shader.UseProgram();
            setUniforms(new Vector2(bounds.X, bounds.Y), color, rotation, origin, new Vector2(bounds.Width, bounds.Height), renderFlags);

            GL.ActiveTexture(TextureUnit.Texture0);
            texture.Bind();

            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.BindVertexArray(0);
        }

        public void DirectDraw(int texture, RectangleF bounds, Color4 color)
        {
            this.DirectDraw(texture, bounds, color, RenderFlags.None);
        }
        public void DirectDraw(int texture, RectangleF bounds, Color4 color, RenderFlags flags)
        {
            shader.UseProgram();
            setUniforms(new Vector2(bounds.X, bounds.Y), color, 0f, Vector2.Zero, new Vector2(bounds.Width, bounds.Height), flags);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture);

            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.BindVertexArray(0);
        }

        public void Dispose()
        {
            this.shader.Dispose();
        }

        private void setUniforms(Vector2 position, Color4 color, float rotation, Vector2 origin, Vector2 size, RenderFlags flags)
        {
            //Matrix
            Matrix4 model =
                Matrix4.CreateScale(size.X, size.Y, 0f) *
                Matrix4.CreateTranslation(-origin.X, -origin.Y, 0f) *
                Matrix4.CreateRotationZ(rotation) *
                Matrix4.CreateTranslation(position.X, position.Y, 0f);
            GL.UniformMatrix4(modelLoc, false, ref model);

            //Color
            GL.Uniform4(colorLoc, color);

            //Render flags
            if ((flags | RenderFlags.None) == RenderFlags.None)
            {
                GL.Uniform1(blurLoc, 0);
                GL.Uniform1(desaturateLoc, 0);
                //SetVertical
                //SetHorizontal
            }
            else
            {
                if ((flags & RenderFlags.Blur) == RenderFlags.Blur)
                {
                    GL.Uniform1(blurLoc, 1);
                    GL.Uniform2(resolutionLoc, size);
                }
                else
                {
                    GL.Uniform1(blurLoc, 0);
                }

                if ((flags & RenderFlags.Desaturate) == RenderFlags.Desaturate)
                {
                    GL.Uniform1(desaturateLoc, 1);
                }
                else
                {
                    GL.Uniform1(desaturateLoc, 0);
                }
                //SetVertical
                //SetHorizontal
            }
        }
    }

    [Flags]
    public enum RenderFlags
    {
        None = 0x0,
        Blur = 0x1,
        Desaturate = 0x2,
        FlipVertically = 0x4,
        FlipHorizontally = 0x8
    }
}
