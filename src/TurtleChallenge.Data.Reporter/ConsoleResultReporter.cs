using System;
using TurtleChallenge.Data.Reporter.Dto;
using TurtleChallenge.Utils.Extensions;

namespace TurtleChallenge.Data.Reporter
{
    public class ConsoleResultReporter : IResultReporter
    {
        public void ReportMoveResults(MovesResult result)
        {
            var message = GetMessage(result);

            Console.WriteLine(message);
        }

        private string GetMessage(MovesResult result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            return $"Sequence {result.SequenceId}: {result.Result.GetDescription()}!";
        }
    }
}
