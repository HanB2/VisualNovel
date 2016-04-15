using Minalear;

namespace DongLife
{
    public static class GameManager
    {
        public static string PlayerName = "Robert";
        public static bool PissedOffJanitor;
        public static string ChosenDate = "MOVIES";
        public static bool DeniedFeelings = false;
        public static string TexturePath = @"Textures/Actors/player_male.png";

        public static GeoRenderer Renderer;

        public static void Init()
        {
            PissedOffJanitor = false;
        }
    }
}
