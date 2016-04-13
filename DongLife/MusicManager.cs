using System.Collections.Generic;
using OpenTK;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using Minalear;

namespace DongLife
{
    public static class MusicManager
    {
        private static AudioPlayer audioPlayer;
        private static ContentManager contentManager;
        private static Dictionary<string, string> songs;

        private static AudioClip currentSong;

        public static void Init(ContentManager content)
        {
            audioPlayer = new AudioPlayer();
            contentManager = content;
            songs = new Dictionary<string, string>();
        }

        public static void RegisterSong(string name, string path)
        {
            songs.Add(name, path);
        }
        public static void PlaySong(string name)
        {
            if (songs.ContainsKey(name))
            {
                audioPlayer.StopBackgroundTrack();
                if (currentSong != null)
                    currentSong.Dispose();

                currentSong = contentManager.LoadAudioFile(songs[name]);
                audioPlayer.PlayBackgroundTrack(currentSong);
            }
        }

        public static void Update(GameTime gameTime)
        {
            audioPlayer.Update(gameTime);
        }
    }
}
