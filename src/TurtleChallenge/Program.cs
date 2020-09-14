using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using TurtleChallenge.Core;
using TurtleChallenge.Core.Controllers;
using TurtleChallenge.Data.Reporter;
using TurtleChallenge.Data.Retriever;
using TurtleChallenge.Data.Retriever.Dto;

namespace TurtleChallenge
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var turtleGameFileSettings = GetFileSettingsFromArguments(args);

            var gameSettings = GetGameSettings(turtleGameFileSettings);

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(turtleGameFileSettings);
            serviceCollection.AddSingleton(gameSettings);

            serviceCollection.AddSingleton<ITurtleGame, TurtleGame>();

            serviceCollection.AddSingleton<IBoardController, BoardController>();
            serviceCollection.AddSingleton<ISequenceController, SequenceController>();
            serviceCollection.AddSingleton<IResultReporter, ConsoleResultReporter>();

            serviceCollection.AddSingleton<ISequencesRetriever, FileSequencesRetriever>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var game = serviceProvider.GetRequiredService<ITurtleGame>();
            game.Play();
        }

        private static GameSettings GetGameSettings(TurtleGameFileSettings turtleGameFileSettings)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile(turtleGameFileSettings.GameSettingsFile, optional: false);
            var configuration = configurationBuilder.Build();
            var gameSettings = configuration.GetSection(nameof(GameSettings)).Get<GameSettings>() ?? throw new ArgumentException("Config: GameSettings");

            Validator.ValidateObject(gameSettings, new ValidationContext(gameSettings), true);

            return gameSettings;
        }

        private static TurtleGameFileSettings GetFileSettingsFromArguments(string[] args)
        {
            string settingsFileName = (args != null && args.Length > 0) ? args[0] : null;
            string movesFileName = (args != null && args.Length > 1) ? args[0] : null;

            Sanitize(settingsFileName, nameof(TurtleGameFileSettings.GameSettingsFile));
            Sanitize(movesFileName, nameof(TurtleGameFileSettings.MovesFile));

            var turtleGameFileSettings = new TurtleGameFileSettings
            {
                GameSettingsFile = settingsFileName,
                MovesFile = movesFileName
            };
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
