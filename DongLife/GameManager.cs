using Minalear;

namespace DongLife
{
    public static class GameManager
    {
        public static bool PissedOffJanitor;
        public static string ChosenDate = "MOVIES";
        public static bool DeniedFeelings = false;

        public static GeoRenderer Renderer;

        public static void Init()
        {
            PissedOffJanitor = false;
        }
    }
}
