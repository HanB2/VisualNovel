﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Minalear
{
    public sealed class Texture2D : IDisposable
    {
        private int textureID;
        private int width;
        private int height;

        public Texture2D(int width, int height, IntPtr data)
        {
            this.width = width;
            this.height = height;

            //Create Texture
            textureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            //GL.TexStorage2D(TextureTarget2d.Texture2D, 1, SizedInternalFormat.Rgba8, width, height); //Specifies the number of mipmaps to generate
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data);
            //GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, width, height, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data);

            //Set wrapper and filter modes
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.MirroredRepeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.MirroredRepeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            //Unbind texture
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void UpdateTexture(IntPtr data)
        {
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            //GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data);
            GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, width, height, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data);

            //Set wrapper and filter modes
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.MirroredRepeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.MirroredRepeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, textureID);
        }

        public void Dispose()
        {
            Delete();
        }
        public void Delete()
        {
            GL.DeleteTexture(textureID);
        }

        [Obsolete("Use ContentManager to load textures.")]
        public static Texture2D LoadFromSource(string @path)
        {
            if (!System.IO.File.Exists(path))
                throw new System.IO.FileNotFoundException(path);

            using (Bitmap bitmap = new Bitmap(path))
            {
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.ActiveTexture(TextureUnit.Texture0);
                Texture2D texture = new Texture2D(bitmap.Width, bitmap.Height, data.Scan0);

                bitmap.UnlockBits(data);

                return texture;
            }
        }

        public int ID { get { return this.textureID; } }
        public int Width { get { return this.width; } }
        public int Height { get { return this.height; } }
    }
}
