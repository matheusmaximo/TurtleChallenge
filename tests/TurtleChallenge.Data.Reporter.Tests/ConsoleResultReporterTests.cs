using System;
using TurtleChallenge.Data.Reporter.Dto;
using TurtleChallenge.Tests.Utils.xUnit.Settings;
using Xunit;

namespace TurtleChallenge.Data.Reporter.Tests
{
    [Trait(Traits.Category, Categories.Unit)]
    public class ConsoleResultReporterTests
    {
        [Fact]
        public void ReportMoveResults_GivenNullResult_ShouldThrowError()
        {
            MovesResult result = null;
            var sut = new ConsoleResultReporter();

            var exception = Record.Exception(() => sut.ReportMoveResults(result));

            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}
