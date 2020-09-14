using System;
using TurtleChallenge.Data.Retriever.Dto;

namespace TurtleChallenge.Core.Controllers
{
    public class TurtleController : ITurtleController
    {
        private readonly Turtle _turtle;
        public Turtle GetTurtle()
        {
            if (_turtle == null)
            {
                throw new ArgumentException($"{nameof(Turtle)} not initialized");
            }

            var position = _turtle.GetPosition();
            var direction = _turtle.Direction;
            return new Turtle(position.X, position.Y, direction);
        }

        public TurtleController(GameSettings gameSettings)
        {
            var x = gameSettings.StartingPosition.X;
            var y = gameSettings.StartingPosition.Y;
            var direction = gameSettings.StartingPosition.Direction;

            _turtle = new Turtle(x, y, direction);
        }
    }
}
