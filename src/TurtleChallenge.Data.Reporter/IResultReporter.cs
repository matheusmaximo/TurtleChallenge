using TurtleChallenge.Data.Reporter.Dto;

namespace TurtleChallenge.Data.Reporter
{
    public interface IResultReporter
    {
        public void ReportMoveResults(MovesResult result);
    }
}
