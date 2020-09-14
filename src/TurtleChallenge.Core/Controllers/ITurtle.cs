using TurtleChallenge.Data.Retriever.Dto;

namespace TurtleChallenge.Core.Controllers
{
    public interface ITurtle
    {
        Position GetPosition();

        void Move();

        void Rotate();
    }
}