using Minalear;
using OpenTK.Graphics;

namespace DongLife
{
    public static class GameManager
    {
        //Appearance
        public static string PlayerName = "Robert";
        public static string ChosenDate = "MOVIES";
        public static string TexturePath = @"Textures/Actors/player_male.png";
        public static int HatIndex = -1;
        public static int ShirtIndex = -1;
        public static int MiscIndex = -1;
        public static Color4 PlayerColor = Color4.White;
        public static string Gender = "Male";

        public static bool PissedOffJanitor = false;
        public static bool DeniedFeelings = false;
        public static bool CompletedHomework = false;
        public static bool TeacherDied = false;

        public static GeoRenderer Renderer;

        public static void ResetDefaults()
        {
            PlayerName = "Robert";
            ChosenDate = "MOVIES";
            TexturePath = @"Textures/Actors/player_male.png";
            HatIndex = -1;
            ShirtIndex = -1;
            MiscIndex = -1;
            PlayerColor = Color4.White;
            Gender = "Male";

            PissedOffJanitor = false;
            DeniedFeelings = false;
            CompletedHomework = false;
            TeacherDied = false;
        }
    }
}
