using Moq;
using System;
using TurtleChallenge.Core.Controllers;
using TurtleChallenge.Data.Reporter;
using TurtleChallenge.TestUtilities.xUnit.Settings;
using Xunit;

namespace TurtleChallenge.Core.Tests
{
    [Trait(Traits.Category, Categories.Unit)]
    public class TurtleGameTests
    {
        [Fact]
        public void Constructor_GivenNullBoardController_ShouldThrowError()
        {
            IBoardController boardController = null;
            ISequenceController sequenceController = new Mock<ISequenceController>().Object;
            IResultReporter resultReporter = new Mock<IResultReporter>().Object;

            var exception = Record.Exception(() => new TurtleGame(boardController, sequenceController, resultReporter));

            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void Constructor_GivenNullSequenceController_ShouldThrowError()
        {
            IBoardController boardController = new Mock<IBoardController>().Object;
            ISequenceController sequenceController = null;
            IResultReporter resultReporter = new Mock<IResultReporter>().Object;

            var exception = Record.Exception(() => new TurtleGame(boardController, sequenceController, resultReporter));

            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void Constructor_GivenNullResultReporter_ShouldThrowError()
        {
            IBoardController boardController = new Mock<IBoardController>().Object;
            ISequenceController sequenceController = new Mock<ISequenceController>().Object;
            IResultReporter resultReporter = null;

            var exception = Record.Exception(() => new TurtleGame(boardController, sequenceController, resultReporter));

            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}
