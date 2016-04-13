using System;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace Minalear
{
    public class AudioPlayer : IDisposable
    {
        private int sourceID;
        private bool loopMusic = true;
        private ALSourceState previousState;

        public AudioPlayer()
        {
            sourceID = AL.GenSource();
        }
        public void Dispose()
        {
            AL.DeleteSource(sourceID);
        }

        public void PlayBackgroundTrack(AudioClip track, bool loop = true)
        {
            loopMusic = loop;
            AL.Source(sourceID, ALSourcei.Buffer, track.ID);
            AL.SourcePlay(sourceID);
        }
        public void StopBackgroundTrack()
        {
            AL.SourceStop(sourceID);
        }

        public void Update(GameTime gameTime)
        {
            ALSourceState state = AL.GetSourceState(sourceID);

            //Playing track stopped
            if (state == ALSourceState.Stopped && previousState == ALSourceState.Playing)
            {
                if (loopMusic)
                    AL.SourcePlay(sourceID);
            }

            previousState = state;
        }
        public void SetVolume(float val)
        {
            AL.Source(sourceID, ALSourcef.Gain, val);
        }
    }
}
