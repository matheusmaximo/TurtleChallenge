using System;
using System.Collections.Generic;
using TurtleChallenge.Core;
using TurtleChallenge.Core.Controllers;
using TurtleChallenge.Core.Tests.Builders;
using TurtleChallenge.Data.Reporter;
using TurtleChallenge.Data.Reporter.Dto;
using TurtleChallenge.Data.Retriever;
using TurtleChallenge.TestUtilities.xUnit.Settings;
using Xunit;

namespace TurtleChallenge.Integration.Tests
{
    [Trait(Traits.Category, Categories.Integration)]
    public class TurtleGameTests
    {
        [Fact]
        public void MultipleSequences_RetrunMultipleResults()
        {
            var resultReporter = new InMemoryResultReporter();

            var turtleGame = GetTurtleGame(resultReporter);

            turtleGame.Play();

            var result1 = resultReporter.Results[0];
            Assert.Equal(MovesResultPossibilities.MineHit, result1.Result);

            var result2 = resultReporter.Results[1];
            Assert.Equal(MovesResultPossibilities.OutOfBoard, result2.Result);

            var result3 = resultReporter.Results[2];
            Assert.Equal(MovesResultPossibilities.InDanger, result3.Result);

            var result4 = resultReporter.Results[3];
            Assert.Equal(MovesResultPossibilities.Success, result4.Result);
        }

        private TurtleGame GetTurtleGame(IResultReporter resultReporter)
        {
            var gameSettings = new GameSettingsBuilder().Build();
            IBoardController boardController = new BoardController(gameSettings);

            ISequencesRetriever sequencesRetriever = new InMemorySequenceRetriever();
            ISequenceController sequenceController = new SequenceController(sequencesRetriever);

            return new TurtleGame(boardController, sequenceController, resultReporter);
        }
    }

    internal class InMemorySequenceRetriever : ISequencesRetriever
    {
        private readonly List<char[]> _moves = new List<char[]>
        {
            new char[] { 'r', 'm' }, //Should hit a min
            new char[] { 'r', 'r', 'r', 'm' }, //Should go out of the board
            new char[] { 'm' }, //Should be still in danger
            new char[] { 'm', 'r', 'm', 'm', 'm', 'm', 'r', 'm', 'm' }, //Should succeed
        };
        private int _moveIndex = 0;

        public char[] GetNextSequenceOfMoves()
        {
            if (_moves.Count <= _moveIndex)
            {
                return Array.Empty<char>();
            }

            var move = _moves[_moveIndex++];

            return move;
        }
    }

    internal class InMemoryResultReporter : IResultReporter
    {
        public List<MovesResult> Results = new List<MovesResult>();
        public void ReportMoveResults(MovesResult result)
        {
            Results.Add(result);
        }
    }
}
