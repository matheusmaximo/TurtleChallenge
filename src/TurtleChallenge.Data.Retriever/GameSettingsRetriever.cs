using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using TurtleChallenge.Data.Retriever.Dto;

namespace TurtleChallenge.Data.Retriever
{
    public static class GameSettingsRetriever
    {
        public static GameSettings GetFromFile(string gameSettingsFile)
        {
            if (!File.Exists(gameSettingsFile))
            {
                Console.WriteLine("GameSettings file not found.");
                Environment.Exit(0);
            }

            try
            {
                string jsonString = GetJsonStringFromFile(gameSettingsFile);

                var gameSettings = GetGameSettingsFromJsonString(jsonString);

                ValidateGameSettings(gameSettings);

                return gameSettings;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load GameSettings file. {ex.Message}");
                Environment.Exit(0);
            }

            return null;
        }

        private static string GetJsonStringFromFile(string gameSettingsFile)
        {
            return File.ReadAllText(gameSettingsFile);
        }

        private static GameSettings GetGameSettingsFromJsonString(string jsonString)
        {
            JsonSerializerOptions deserializeOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = true
            };
            deserializeOptions.Converters.Add(new JsonStringEnumConverter());
            return JsonSerializer.Deserialize<GameSettings>(jsonString, deserializeOptions);
        }

        private static void ValidateGameSettings(GameSettings gameSettings)
        {
            Validator.ValidateObject(gameSettings, new ValidationContext(gameSettings), validateAllProperties: true);
            Validator.ValidateObject(gameSettings.BoardSize, new ValidationContext(gameSettings.BoardSize), validateAllProperties: true);
            Validator.ValidateObject(gameSettings.ExitPoint, new ValidationContext(gameSettings.ExitPoint), validateAllProperties: true);
            Validator.ValidateObject(gameSettings.StartingPosition, new ValidationContext(gameSettings.StartingPosition), validateAllProperties: true);
            foreach (var mine in gameSettings.Mines)
            {
                Validator.ValidateObject(mine, new ValidationContext(mine), validateAllProperties: true);
            }
        }
    }
}
