using System;

namespace DongLife
{
    public class Program
    {
        private static MainGame game;

        public static void Main(string[] args)
        {
            ExecuteGame();
        }

        public static void RestartGame()
        {
            game.Window.Exit();
            ExecuteGame();
        }
        public static void ExecuteGame()
        {
            GameSettings.LoadSettings("settings.ini");
            using (game = new MainGame(GameSettings.WindowWidth, GameSettings.WindowHeight))
            {
                game.Run();
            }
        }
    }
}
