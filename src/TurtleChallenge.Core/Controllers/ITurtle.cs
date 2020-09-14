using TurtleChallenge.Data.Retriever.Dto;

namespace TurtleChallenge.Core.Controllers
{
    public interface ITurtle
    {
        Point GetPosition();

        void Move();

        void Rotate();
    }
}