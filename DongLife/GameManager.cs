using Minalear;
using OpenTK.Graphics;

namespace DongLife
{
    public static class GameManager
    {
        //Appearance
        public static string PlayerName = "Robert";
        public static string JaegerName = "Robert";
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
        public static bool AttendedClass = false;
        public static bool HadDinner = false;
        public static bool BlamedJaegers = false;
        public static bool RobbedJewelryStore = false;
        public static bool PoopedOnCounter = false;

        public static GeoRenderer Renderer;

        public static void ResetDefaults()
        {
            PlayerName = "Robert";
            JaegerName = "Robert";
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
            AttendedClass = false;
            HadDinner = false;
            BlamedJaegers = false;
            RobbedJewelryStore = false;
            PoopedOnCounter = false;
        }

        public static void GenerateJaegerName()
        {
            string[] JaegerTitles = System.IO.File.ReadAllLines(@"Content/Data/jaeger_titles.txt");
            JaegerName = string.Format("{0} Jaeger", JaegerTitles[RNG.Next(0, JaegerTitles.Length)]);
        }
    }
}
