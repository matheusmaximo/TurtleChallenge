using System;
using TurtleChallenge.Core.Controllers.BoardParts;
using TurtleChallenge.Core.Exceptions;
using TurtleChallenge.Data.Reporter.Dto;
using TurtleChallenge.Data.Retriever.Dto;

namespace TurtleChallenge.Core.Controllers
{
    public class BoardController : IBoardController
    {
        private readonly ITilesController _tilesController;
        private readonly ITurtleController _turtleController;


        public BoardController(ITilesController tilesController, ITurtleController turtleController)
        {
            _tilesController = tilesController ?? throw new ArgumentNullException(nameof(tilesController));
            _turtleController = turtleController ?? throw new ArgumentNullException(nameof(turtleController));

            ValidateTurtlePosition();
        }

        private void ValidateTurtlePosition()
        {
            var turtlePosition = _turtleController.GetTurtle().GetPosition();
            var x = turtlePosition.X;
            var y = turtlePosition.Y;

            var tile = _tilesController.GetTile(x, y);

            if (tile == null)
            {
                throw new IndexOutOfBoardException(nameof(GameSettings.StartingPosition), x, y);
            }

            if (tile is Tile)
            {
                throw new IndexOcupiedException(nameof(GameSettings.StartingPosition), x, y);
            }
        }

        public void Reset()
        {
            int x = _gameSettings.StartingPosition.X ?? 0;
            int y = _gameSettings.StartingPosition.Y ?? 0;
            var direction = _gameSettings.StartingPosition.Direction ?? Directions.North;

            _turtle = new Turtle(x, y, direction);
        }

        public MovesResultPossibilities MoveTurtle()
        {
            if (_turtle == null)
            {
                throw new ArgumentException($"{nameof(Turtle)} not initialized");
            }

            _turtle.Move();

            var position = _turtle.GetPosition();

            int x = position.X;
            int y = position.Y;

            if (!IsInsideBoard(x, y))
            {
                return MovesResultPossibilities.OutOfBoard;
            }

            if (IsOverMine(x, y))
            {
                return MovesResultPossibilities.MineHit;
            }

            return IsOverExit(x, y) ? MovesResultPossibilities.Success : MovesResultPossibilities.InDanger;
        }

        public MovesResultPossibilities GetResult()
        {
            if (_turtle == null)
            {
                throw new ArgumentException($"{nameof(Turtle)} not initialized");
            }

            var position = _turtle.GetPosition();

            int x = position.X;
            int y = position.Y;

            return IsOverExit(x, y) ? MovesResultPossibilities.Success : MovesResultPossibilities.InDanger;
        }

        public void RotateTurtle()
        {
            _turtle?.Rotate();
        }
    }
}
