using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace DongLife
{
    public static class GameSettings
    {
        private static Dictionary<string, object> settings;

        public static void LoadSettings(string filePath)
        {
            if (!File.Exists(filePath))
                generateSettingsFile(filePath);

            settings = new Dictionary<string, object>();
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (String.IsNullOrEmpty(line) || line.Contains("##"))
                    continue; //Ignore Comment/Empty lines
                string[] tokens = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                checkValidTokens(tokens);

                settings.Add(tokens[0].Trim(), tokens[1].Trim());
            }

            WindowWidth = GetSetting<int>("WindowWidth");
            WindowHeight = GetSetting<int>("WindowHeight");
            PlayerName = GetSetting<string>("Name");
        }

        public static T GetSetting<T>(string token)
        {
            return (T)Convert.ChangeType(settings[token], typeof(T));
        }

        private static void generateSettingsFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(filePath)))
            {
                writer.WriteLine(String.Format("## Graphic Settings ##"));
                writer.WriteLine(String.Format("WindowWidth:{0}", 1280));
                writer.WriteLine(String.Format("WindowHeight:{0}", 720));
                writer.WriteLine(String.Format("TextSpeed:{0}", 25));
                writer.WriteLine(String.Format("Name:{0}", "Robert"));
            }
        }
        private static void checkValidTokens(string[] tokens)
        {
            if (tokens.Length != 2)
            {
                if (tokens.Length == 1)
                    throw new InvalidSettingsFileException("Invalid Setting in the 'settings.ini' folder: " + tokens[0]);
                else
                    throw new InvalidSettingsFileException("Settings file is invalid.  Try deleting or contact support.");
            }
        }

        public static int WindowWidth { get; private set; }
        public static int WindowHeight { get; private set; }
        public static string PlayerName { get; set; }
    }

    public class InvalidSettingsFileException : Exception
    {
        public InvalidSettingsFileException() : base() { }
        public InvalidSettingsFileException(string message) : base(message) { }
        public InvalidSettingsFileException(string message, Exception inner) : base(message, inner) { }
    }
}
