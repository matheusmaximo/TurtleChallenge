using TurtleChallenge.Data.Retriever;

namespace TurtleChallenge.Core.Controllers
{
    public class SequenceController : ISequenceController
    {
        private readonly ISequencesRetriever _sequencesRetriever;

        private int _nextSequenceId = 0;

        public SequenceController(ISequencesRetriever sequencesRetriever)
        {
            _sequencesRetriever = sequencesRetriever ?? throw new System.ArgumentNullException(nameof(sequencesRetriever));
        }

        public ISequence? LoadSequence()
        {
            char[] moves = _sequencesRetriever.GetNextSequenceOfMoves();

            return moves.Length == 0 ? null : (ISequence)new Sequence(_nextSequenceId++, moves);
        }
    }
}
