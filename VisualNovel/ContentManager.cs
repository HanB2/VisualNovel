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
            ContentPath = "Content";
        }
        
        public Texture2D LoadTexture2D(string @path)
        {
            path = getFullPath(path);
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
            vertexPath = getFullPath(vertexPath);
            fragmentPath = getFullPath(fragmentPath);

            checkForValidPath(vertexPath);
            checkForValidPath(fragmentPath);

            Shader shader = new Shader();

            shader.LoadSource(TK.ShaderType.VertexShader, File.ReadAllText(vertexPath));
            shader.LoadSource(TK.ShaderType.FragmentShader, File.ReadAllText(fragmentPath));
            shader.LinkProgram();

            return shader;
        }
        public AudioClip LoadAudioFile(string @path)
        {
            //Only supports a very specific type of .wav file
            //Code borrowed from OpenTK Examples - https://github.com/opentk/opentk/blob/develop/Source/Examples/OpenAL/1.1/Playback.cs
            path = getFullPath(path);
            checkForValidPath(path);

            AudioClip clip;

            using (Stream fileStream = File.Open(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    //RIFF Header
                    string signature = new string(reader.ReadChars(4));
                    if (signature != "RIFF")
                        throw new NotSupportedException("Specified file is not a WAVE file.");

                    int riff_chunk_size = reader.ReadInt32();

                    string format = new string(reader.ReadChars(4));
                    if (format != "WAVE")
                        throw new NotSupportedException("Specified file is not a WAVE file.");

                    //Wave Header
                    string format_signature = new string(reader.ReadChars(4));
                    if (format_signature != "fmt ")
                        throw new NotSupportedException("Specified WAVE file is not supported.");

                    int format_chunk_size = reader.ReadInt32();
                    int audio_format = reader.ReadInt16();
                    int num_channels = reader.ReadInt16();
                    int sample_rate = reader.ReadInt32();
                    int byte_rate = reader.ReadInt32();
                    int block_align = reader.ReadInt16();
                    int bits_per_sample = reader.ReadInt16();

                    string data_signature = new string(reader.ReadChars(4));
                    if (data_signature != "data")
                        throw new NotSupportedException("Specified WAVE file is not supported.");

                    int data_chunk_size = reader.ReadInt32();

                    byte[] dataStream = reader.ReadBytes(data_chunk_size);
                    clip = new AudioClip(dataStream, num_channels, bits_per_sample, sample_rate);
                }
            }

            return clip;
        }

        private void checkForValidPath(string @path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("ASSET NOT FOUND: " + path);
        }
        private string getFullPath(string @path)
        {
            return ContentPath + "/" + path;
        }
    }
}
