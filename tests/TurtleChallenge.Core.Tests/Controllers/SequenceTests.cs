using Moq;
using System;
using TurtleChallenge.Core.Controllers;
using TurtleChallenge.Core.Exceptions;
using TurtleChallenge.Data.Reporter.Dto;
using TurtleChallenge.TestUtilities.xUnit.Settings;
using Xunit;

namespace TurtleChallenge.Core.Tests.Controllers
{
    [Trait(Traits.Category, Categories.Unit)]
    public class SequenceTests
    {
        [Fact]
        public void Constructor_GivenNullMoves_ShouldThrowError()
        {
            int sequenceId = 0;
            char[] moves = null;

            var exception = Record.Exception(() => new Sequence(sequenceId, moves));

            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void ExecuteMoves_GivenBoardControllerAndMoves_ShouldMoveTurtle()
        {
            int sequenceId = 0;
            var moves = new char[] { 'm', 'r', 'm' };

            Mock<IBoardController> mockBoardController = new Mock<IBoardController>();
            IBoardController boardController = mockBoardController.Object;

            var sut = new Sequence(sequenceId, moves);
            sut.ExecuteMoves(boardController);

            mockBoardController.Verify(m => m.MoveTurtle(), Times.Exactly(2));
            mockBoardController.Verify(m => m.RotateTurtle(), Times.Once);
        }

        [Fact]
        public void ExecuteMoves_GivenBoardControllerAndMoves_ReturnResult()
        {
            int sequenceId = 0;
            var moves = new char[] { 'm', 'r', 'm' };
            var movesResult = MovesResultPossibilities.Success;

            Mock<IBoardController> mockBoardController = new Mock<IBoardController>();
            mockBoardController.Setup(m => m.GetResult()).Returns(movesResult);
            IBoardController boardController = mockBoardController.Object;

            var sut = new Sequence(sequenceId, moves);
            var result = sut.ExecuteMoves(boardController);

            Assert.Equal(movesResult, result.Result);
            Assert.Equal(sequenceId, result.SequenceId);
        }

        [Fact]
        public void ExecuteMoves_WhenHitMine_ReturnMineHit()
        {
            int sequenceId = 0;
            var moves = new char[] { 'm' };
            var movesResult = MovesResultPossibilities.MineHit;

            Mock<IBoardController> mockBoardController = new Mock<IBoardController>();
            mockBoardController.Setup(m => m.MoveTurtle()).Throws<TurtleHitMineException>();
            IBoardController boardController = mockBoardController.Object;

            var sut = new Sequence(sequenceId, moves);
            var result = sut.ExecuteMoves(boardController);

            Assert.Equal(movesResult, result.Result);
        }

        [Fact]
        public void ExecuteMoves_WhenMoveOutOfBoard_ReturnOutOfBoard()
        {
            int sequenceId = 0;
            var moves = new char[] { 'm' };
            var movesResult = MovesResultPossibilities.OutOfBoard;

            Mock<IBoardController> mockBoardController = new Mock<IBoardController>();
            var dummyException = new IndexOutOfBoardException("Turtle", 0, 0);
            mockBoardController.Setup(m => m.MoveTurtle()).Throws(dummyException);
            IBoardController boardController = mockBoardController.Object;

            var sut = new Sequence(sequenceId, moves);
            var result = sut.ExecuteMoves(boardController);

            Assert.Equal(movesResult, result.Result);
        }
    }
}
