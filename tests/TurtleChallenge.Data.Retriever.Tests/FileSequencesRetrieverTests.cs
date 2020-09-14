using System;
using TurtleChallenge.Data.Retriever.Dto;
using TurtleChallenge.TestUtilities.xUnit.Settings;
using Xunit;

namespace TurtleChallenge.Data.Retriever.Tests
{
    [Trait(Traits.Category, Categories.Unit)]
    public class FileSequencesRetrieverTests
    {
        [Fact]
        public void Constructor_GivenNullSettings_ShouldThrowError()
        {
            TurtleGameFileSettings settings = null;

            var exception = Record.Exception(() => new FileSequencesRetriever(settings));

            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}
