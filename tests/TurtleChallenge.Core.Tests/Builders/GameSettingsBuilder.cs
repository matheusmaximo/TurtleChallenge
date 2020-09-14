using System.Collections.Generic;
using TurtleChallenge.Data.Retriever.Dto;

namespace TurtleChallenge.Core.Tests.Builders
{
    public class GameSettingsBuilder
    {
        private int _startingPositionX = 0;
        private int _startingPositionY = 2;
        private Directions _direction = Directions.North;
        private Point _exitPoint = new Point
        {
            X = 4,
            Y = 1
        };

        public GameSettings Build()
        {
            return new GameSettings
            {
                BoardSize = new Board
                {
                    M = 4,
                    N = 5
                },
                ExitPoint = _exitPoint,
                StartingPosition = new StartingPosition
                {
                    X = _startingPositionX,
                    Y = _startingPositionY,
                    Direction = _direction
                },
                Mines = new List<Point>
                {
                    new Point
                    {
                        X = 1,
                        Y = 2
                    },
                    new Point
                    {
                        X = 3,
                        Y = 0
                    },
                    new Point
                    {
                        X = 3,
                        Y = 2
                    }
                }
            };
        }

        public GameSettingsBuilder WithStartingPositionDirection(Directions newDirection)
        {
            _direction = newDirection;

            return this;
        }

        public GameSettingsBuilder WithStartingPositionX(int newX)
        {
            _startingPositionX = newX;

            return this;
        }

        public GameSettingsBuilder WithStartingPositionY(int newY)
        {
            _startingPositionY = newY;

            return this;
        }

        public GameSettingsBuilder WithExitPoint(Point newExitPoint)
        {
            _exitPoint = newExitPoint;

            return this;
        }
    }
}
