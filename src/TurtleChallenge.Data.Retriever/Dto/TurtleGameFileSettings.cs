namespace TurtleChallenge.Data.Retriever.Dto
{
    public class TurtleGameFileSettings
    {
        public TurtleGameFileSettings(string gameSettingsFile, string movesFile)
        {
            GameSettingsFile = gameSettingsFile;
            MovesFile = movesFile;
        }

        public string GameSettingsFile { get; set; }
        public string MovesFile { get; set; }
    }
}
