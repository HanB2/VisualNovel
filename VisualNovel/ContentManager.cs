using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using TK = OpenTK.Graphics.OpenGL;

namespace Minalear
{
    public class ContentManager
    {
        public string ContentPath { get; set; }

        public ContentManager()
        {
            ContentPath = "Content/";
        }

        //TODO: Possible memory leak with unmanaged resources, double check everything
        public Texture2D LoadTexture2D(string @path)
        {
            path = ContentPath + path;
            checkForValidPath(path);

            Bitmap bitmap = new Bitmap(path);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            TK.GL.ActiveTexture(TK.TextureUnit.Texture0);
            Texture2D texture = new Texture2D(bitmap.Width, bitmap.Height, data.Scan0);

            bitmap.UnlockBits(data);
            bitmap.Dispose();

            return texture;
        }
        public Shader LoadShader(string @vertexPath, string @fragmentPath)
        {
            Shader shader = new Shader();

            shader.LoadSource(TK.ShaderType.VertexShader, File.ReadAllText(ContentPath + vertexPath));
            shader.LoadSource(TK.ShaderType.FragmentShader, File.ReadAllText(ContentPath + fragmentPath));
            shader.LinkProgram();

            return shader;
        }

        private void checkForValidPath(string @path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("ASSET NOT FOUND: " + path);
        }
    }
}
