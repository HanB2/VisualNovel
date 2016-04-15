using Minalear;
using OpenTK.Graphics;

namespace DongLife
{
    public static class GameManager
    {
        public static string PlayerName = "Robert";
        public static bool PissedOffJanitor = false;
        public static string ChosenDate = "MOVIES";
        public static bool DeniedFeelings = false;
        public static string TexturePath = @"Textures/Actors/player_male.png";
        public static int HatIndex = -1;
        public static int ShirtIndex = -1;
        public static int MiscIndex = -1;
        public static Color4 PlayerColor = Color4.White;
        public static string Gender = "Male";

        public static GeoRenderer Renderer;
    }
}
