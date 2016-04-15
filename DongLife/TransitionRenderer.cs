using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Minalear;

namespace DongLife
{
    public class TransitionRenderer
    {
        private Shader shader;
        private int vao;

        public TransitionRenderer(Shader shader, int width, int height)
        {
            this.shader = shader;

            float[] vertices = new float[] {
                0f, 0f,     0f, 1f,     1f, 0f,
                1f, 0f,     0f, 1f,     1f, 1f,
            };

            //Shader setup
            vao = GL.GenVertexArray();
            int vbo = GL.GenBuffer();

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * sizeof(float)), vertices, BufferUsageHint.StaticDraw);

            //Attributes
            shader.UseProgram();
            int posAttrib = GL.GetAttribLocation(shader.ID, "pos");
            GL.EnableVertexAttribArray(posAttrib);
            GL.VertexAttribPointer(posAttrib, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);

            //Uniforms
            int projLoc = GL.GetUniformLocation(shader.ID, "proj");
            Matrix4 projMat = Matrix4.CreateOrthographicOffCenter(0f, width, height, 0f, -1f, 1f);
            GL.UniformMatrix4(projLoc, false, ref projMat);

            int modelLoc = GL.GetUniformLocation(shader.ID, "model");
            Matrix4 modelMat = Matrix4.CreateScale(width, height, 0f);
            GL.UniformMatrix4(modelLoc, false, ref modelMat);

            //Textures
            int baseTexLoc = GL.GetUniformLocation(shader.ID, "baseTex");
            GL.Uniform1(baseTexLoc, 0);

            int destTexLoc = GL.GetUniformLocation(shader.ID, "destTex");
            GL.Uniform1(destTexLoc, 1);

            int tranTexLoc = GL.GetUniformLocation(shader.ID, "tranTex");
            GL.Uniform1(tranTexLoc, 2);
        }

        public void DrawTransition(int baseTex, int destTex, int tranTex)
        {
            shader.UseProgram();

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, baseTex);

            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture2D, destTex);

            GL.ActiveTexture(TextureUnit.Texture2);
            GL.BindTexture(TextureTarget.Texture2D, tranTex);

            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.BindVertexArray(0);
        }
    }
}
