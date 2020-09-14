using TurtleChallenge.Core.Exceptions;
using TurtleChallenge.Data.Reporter.Dto;
using TurtleChallenge.Data.Retriever.Dto;

namespace TurtleChallenge.Core.Controllers
{
    public class Sequence : ISequence
    {
        public int SequenceId { get; }

        private readonly char[] _moves;

        public Sequence(int sequenceId, char[] moves)
        {
            SequenceId = sequenceId;
            _moves = moves ?? throw new System.ArgumentNullException(nameof(moves));
        }

        public MovesResult ExecuteMoves(IBoardController board)
        {
            MovesResultPossibilities? movesResult;
            try
            {
                foreach (var move in _moves)
                {
                    switch ((Moves)move)
                    {
                        case Moves.Move:
                            board.MoveTurtle();
                            break;
                        case Moves.Rotate:
                            board.RotateTurtle();
                            break;
                    }
                }

                movesResult = board.GetResult();
            }
            catch (TurtleHitMineException)
            {
                movesResult = MovesResultPossibilities.MineHit;
            }
            catch (IndexOutOfBoardException)
            {
                movesResult = MovesResultPossibilities.OutOfBoard;
            }

            return new MovesResult
            {
                SequenceId = SequenceId,
                Result = (MovesResultPossibilities)movesResult
            };
        }
    }
}
