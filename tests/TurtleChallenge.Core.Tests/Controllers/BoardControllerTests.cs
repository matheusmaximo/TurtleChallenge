using System;
using TurtleChallenge.Core.Controllers;
using TurtleChallenge.Core.Controllers.BoardParts;
using TurtleChallenge.Core.Exceptions;
using TurtleChallenge.Core.Tests.Builders;
using TurtleChallenge.Data.Reporter.Dto;
using TurtleChallenge.Data.Retriever.Dto;
using TurtleChallenge.TestUtilities.xUnit.Settings;
using Xunit;

namespace TurtleChallenge.Core.Tests.Controllers
{
    [Trait(Traits.Category, Categories.Unit)]
    public class BoardControllerTests
    {
        [Fact]
        public void Constructor_GivenNullGameSettings_ShouldThrowError()
        {
            GameSettings gameSettings = null;

            var exception = Record.Exception(() => new BoardController(gameSettings));

            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void Constructor_GivenValidGameSettings_ShouldCreateTiles()
        {
            var gameSettings = new GameSettingsBuilder().Build();

            var sut = new BoardController(gameSettings);

            var result = sut.GetTiles();

            Assert.NotNull(result);
            Assert.Equal(gameSettings.BoardSize.N, result.GetLength(0));
            Assert.Equal(gameSettings.BoardSize.M, result.GetLength(1));
        }

        [Fact]
        public void Constructor_GivenValidGameSettings_ShouldSetupExit()
        {
            var gameSettings = new GameSettingsBuilder().Build();

            var sut = new BoardController(gameSettings);

            var result = sut.GetTiles();
            var exitTile = result[(int)gameSettings.ExitPoint.X, (int)gameSettings.ExitPoint.Y];

            Assert.IsType<ExitTile>(exitTile);
        }

        [Fact]
        public void Constructor_GivenValidGameSettings_ShouldSetupMines()
        {
            var gameSettings = new GameSettingsBuilder().Build();

            var sut = new BoardController(gameSettings);

            var result = sut.GetTiles();
            var firstMine = result[(int)gameSettings.Mines[0].X, (int)gameSettings.Mines[0].Y];
            var secondMine = result[(int)gameSettings.Mines[1].X, (int)gameSettings.Mines[1].Y];
            var thirdMine = result[(int)gameSettings.Mines[2].X, (int)gameSettings.Mines[2].Y];

            Assert.IsType<MineTile>(firstMine);
            Assert.IsType<MineTile>(secondMine);
            Assert.IsType<MineTile>(thirdMine);
        }

        [Fact]
        public void Constructor_ShouldSetupTurtle()
        {
            var gameSettings = new GameSettingsBuilder().Build();

            var sut = new BoardController(gameSettings);

            var result = sut.GetTurtle();
            var turtlePosition = result.GetPosition();

            Assert.NotNull(result);
            Assert.Equal(gameSettings.StartingPosition.Direction, result.Direction);
            Assert.Equal(gameSettings.StartingPosition.X, turtlePosition.X);
            Assert.Equal(gameSettings.StartingPosition.Y, turtlePosition.Y);
        }

        [Fact]
        public void MoveTurtle_ShouldMoveTurtle()
        {
            var gameSettings = new GameSettingsBuilder()
                .Build();

            var sut = new BoardController(gameSettings);
            sut.MoveTurtle();

            var result = sut.GetTurtle();
            var turtlePosition = result.GetPosition();

            Assert.Equal(gameSettings.StartingPosition.Direction, result.Direction);
            Assert.Equal(gameSettings.StartingPosition.X, turtlePosition.X);
            Assert.Equal(gameSettings.StartingPosition.Y + 1, turtlePosition.Y);
        }

        [Fact]
        public void MoveTurtle_WhenMoveOutOfBoard_ShouldThrowException()
        {
            var gameSettings = new GameSettingsBuilder()
                .WithStartingPositionDirection(Directions.West)
                .Build();

            var sut = new BoardController(gameSettings);

            var exception = Record.Exception(() => sut.MoveTurtle());

            Assert.IsType<IndexOutOfBoardException>(exception);
        }

        [Fact]
        public void MoveTurtle_WhenMoveToMine_ShouldThrowException()
        {
            var gameSettings = new GameSettingsBuilder()
                .WithStartingPositionDirection(Directions.East)
                .Build();

            var sut = new BoardController(gameSettings);

            var exception = Record.Exception(() => sut.MoveTurtle());

            Assert.IsType<TurtleHitMineException>(exception);
        }

        [Fact]
        public void RotateTurtle_ShouldRotateTurtle()
        {
            var initialDirection = Directions.North;
            var expectedDirection = Directions.East;

            var gameSettings = new GameSettingsBuilder()
                .WithStartingPositionDirection(initialDirection)
                .Build();

            var sut = new BoardController(gameSettings);
            sut.RotateTurtle();

            var result = sut.GetTurtle();

            Assert.Equal(expectedDirection, result.Direction);
        }

        [Fact]
        public void Reset_ShouldMoveTurtleToStartingPosition()
        {
            var gameSettings = new GameSettingsBuilder()
                .Build();

            var sut = new BoardController(gameSettings);
            sut.MoveTurtle();
            sut.Reset();

            var result = sut.GetTurtle();
            var turtlePosition = result.GetPosition();

            Assert.Equal(gameSettings.StartingPosition.Direction, result.Direction);
            Assert.Equal(gameSettings.StartingPosition.X, turtlePosition.X);
            Assert.Equal(gameSettings.StartingPosition.Y, turtlePosition.Y);
        }

        [Fact]
        public void GetResult_WhenTurtleNotInExit_ShouldReturnInDanger()
        {
            var gameSettings = new GameSettingsBuilder()
                .Build();

            var sut = new BoardController(gameSettings);

            var result = sut.GetResult();
            Assert.Equal(MovesResultPossibilities.InDanger, result);
        }

        [Fact]
        public void GetResult_WhenTurtleInExit_ShouldReturnSuccess()
        {
            var exitPoint = new Point
            {
                X = 0,
                Y = 1
            };
            var gameSettings = new GameSettingsBuilder()
                .WithStartingPositionDirection(Directions.North)
                .WithStartingPositionX(0)
                .WithStartingPositionY(0)
                .WithExitPoint(exitPoint)
                .Build();

            var sut = new BoardController(gameSettings);
            sut.MoveTurtle();

            var result = sut.GetResult();
            Assert.Equal(MovesResultPossibilities.Success, result);
        }
    }
}
