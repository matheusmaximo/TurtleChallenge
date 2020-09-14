using Moq;
using System;
using TurtleChallenge.Core.Controllers;
using TurtleChallenge.Data.Retriever;
using TurtleChallenge.TestUtilities.xUnit.Settings;
using Xunit;

namespace TurtleChallenge.Core.Tests.Controllers
{
    [Trait(Traits.Category, Categories.Unit)]
    public class SequenceControllerTests
    {
        [Fact]
        public void Constructor_GivenNullSequencesRetriever_ShouldThrowError()
        {
            ISequencesRetriever sequencesRetriever = null;

            var exception = Record.Exception(() => new SequenceController(sequencesRetriever));

            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void LoadSequence_GivenEmptyMoves_ReturnNull()
        {
            char[] moves = Array.Empty<char>();

            Mock<ISequencesRetriever> mockSequencesRetriever = new Mock<ISequencesRetriever>();
            mockSequencesRetriever.Setup(m => m.GetNextSequenceOfMoves()).Returns(moves);
            ISequencesRetriever sequencesRetriever = mockSequencesRetriever.Object;

            var sut = new SequenceController(sequencesRetriever);
            var result = sut.LoadSequence();

            Assert.Null(result);
        }

        [Fact]
        public void LoadSequence_GivenMoves_ReturnSequence()
        {
            var moves = new char[] { 'a' };

            Mock<ISequencesRetriever> mockSequencesRetriever = new Mock<ISequencesRetriever>();
            mockSequencesRetriever.Setup(m => m.GetNextSequenceOfMoves()).Returns(moves);
            ISequencesRetriever sequencesRetriever = mockSequencesRetriever.Object;

            var sut = new SequenceController(sequencesRetriever);
            var result = sut.LoadSequence();

            Assert.IsType<Sequence>(result);
        }

        [Fact]
        public void LoadSequence_GivenMoreMoves_ReturnSequenceIdIncremented()
        {
            var moves = new char[] { 'a' };

            Mock<ISequencesRetriever> mockSequencesRetriever = new Mock<ISequencesRetriever>();
            mockSequencesRetriever.Setup(m => m.GetNextSequenceOfMoves()).Returns(moves);
            ISequencesRetriever sequencesRetriever = mockSequencesRetriever.Object;

            var sut = new SequenceController(sequencesRetriever);
            var firstResult = (Sequence)sut.LoadSequence();
            var secondResult = (Sequence)sut.LoadSequence();

            Assert.Equal(firstResult.SequenceId + 1, secondResult.SequenceId);
        }
    }
}
