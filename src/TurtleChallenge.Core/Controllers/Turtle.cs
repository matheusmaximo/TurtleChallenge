using TurtleChallenge.Data.Retriever.Dto;

namespace TurtleChallenge.Core.Controllers
{
    public class Turtle : ITurtle
    {
        public Directions Direction { get; private set; }
        private int _x;
        private int _y;

        public Turtle(int x, int y, Directions direction)
        {
            _x = x;
            _y = y;
            Direction = direction;
        }

        public Point GetPosition()
        {
            return new Point
            {
                X = _x,
                Y = _y
            };
        }

        public void Move()
        {
            switch (Direction)
            {
                case Directions.North:
                    _y++;
                    break;
                case Directions.East:
                    _x++;
                    break;
                case Directions.South:
                    _y--;
                    break;
                case Directions.West:
                    _x--;
                    break;
            }
        }

        public void Rotate()
        {
            Direction = Direction == Directions.West ? Directions.North : Direction + 1;
        }
    }
}