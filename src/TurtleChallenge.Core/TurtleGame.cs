using TurtleChallenge.Core.Controllers;
using TurtleChallenge.Data.Reporter;

namespace TurtleChallenge.Core
{
    public class TurtleGame : ITurtleGame
    {
        private readonly IBoardController _boardController;
        private readonly ISequenceController _sequenceController;
        private readonly IResultReporter _resultReporter;

        public TurtleGame(IBoardController boardController, ISequenceController sequenceController, IResultReporter resultReporter)
        {
            _boardController = boardController ?? throw new System.ArgumentNullException(nameof(boardController));
            _sequenceController = sequenceController ?? throw new System.ArgumentNullException(nameof(sequenceController));
            _resultReporter = resultReporter ?? throw new System.ArgumentNullException(nameof(resultReporter));
        }

        public void Play()
        {
            while (_sequenceController.LoadSequence() is ISequence sequence)
            {
                _boardController.Reset();

                var result = sequence.ExecuteMoves(_boardController);

                _resultReporter.ReportMoveResults(result);
            }
        }
    }
}
