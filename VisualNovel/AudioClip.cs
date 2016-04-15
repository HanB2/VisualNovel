using System;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace Minalear
{
    public class AudioClip : IDisposable
    {
        private int bufferID;

        public AudioClip(byte[] buffer, int channels, int bitsPerSample, int sampleRate)
        {
            Channels = channels;
            BitsPerSample = bitsPerSample;
            SampleRate = sampleRate;

            setFormat();

            //OpenAL init
            bufferID = AL.GenBuffer();
            AL.BufferData(bufferID, ClipFormat, buffer, buffer.Length, SampleRate);

            ID = bufferID;
        }
        public void Dispose()
        {
            AL.DeleteBuffer(bufferID);
        }

        private void setFormat()
        {
            switch (Channels)
            {
                case 1: ClipFormat = BitsPerSample == 8 ? ALFormat.Mono8 : ALFormat.Mono16; break;
                case 2: ClipFormat = BitsPerSample == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16; break;
                default: throw new System.InvalidOperationException("Error initializing Audio Clip.  Invalid Format.  Check Content Loading for errors.");
            }
        }

        public int ID { get; set; }
        public int Channels { get; set; }
        public int BitsPerSample { get; set; }
        public int SampleRate { get; set; }
        public ALFormat ClipFormat { get; set; }
    }
}
