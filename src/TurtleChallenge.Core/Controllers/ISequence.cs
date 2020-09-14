using TurtleChallenge.Data.Reporter.Dto;

namespace TurtleChallenge.Core.Controllers
{
    public interface ISequence
    {
        MovesResult ExecuteMoves(IBoardController board);
    }
}
