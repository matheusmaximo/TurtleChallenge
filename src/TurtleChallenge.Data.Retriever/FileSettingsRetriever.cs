using System;
using System.IO;
using System.Linq;
using TurtleChallenge.Data.Retriever.Dto;

namespace TurtleChallenge.Data.Retriever
{
    public static class FileSettingsRetriever
    {
        public static TurtleGameFileSettings GetFromArguments(string[] args)
        {
            string settingsFileName = args != null && args.Length > 0 ? args[0] : throw new ArgumentException("Game Settings file not specified");
            string movesFileName = args.Length > 1 ? args[1] : throw new ArgumentException("Moves file not specified");

            Sanitize(settingsFileName, nameof(TurtleGameFileSettings.GameSettingsFile));
            Sanitize(movesFileName, nameof(TurtleGameFileSettings.MovesFile));

            var turtleGameFileSettings = new TurtleGameFileSettings(settingsFileName, movesFileName);
            return turtleGameFileSettings;
        }

        private static void Sanitize(string filePath, string file)
        {
            if (filePath.Any(c => Path.GetInvalidPathChars().Contains(c)))
            {
                throw new ArgumentException($"File path {file} contains invalid characters.");
            }
        }
    }
}
