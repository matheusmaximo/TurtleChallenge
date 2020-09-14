using Microsoft.Extensions.DependencyInjection;
using TurtleChallenge.Core;
using TurtleChallenge.Core.Controllers;
using TurtleChallenge.Data.Reporter;
using TurtleChallenge.Data.Retriever;

namespace TurtleChallenge
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            var turtleGameFileSettings = FileSettingsRetriever.GetFromArguments(args);

            serviceCollection.AddSingleton(turtleGameFileSettings);

            var gameSettings = GameSettingsRetriever.GetFromFile(turtleGameFileSettings.GameSettingsFile);
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
    }
}
