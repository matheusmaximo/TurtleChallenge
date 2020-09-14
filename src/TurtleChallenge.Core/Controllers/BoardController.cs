using System;
using TurtleChallenge.Core.Controllers.BoardParts;
using TurtleChallenge.Core.Exceptions;
using TurtleChallenge.Data.Reporter.Dto;
using TurtleChallenge.Data.Retriever.Dto;

namespace TurtleChallenge.Core.Controllers
{
    public class BoardController : IBoardController
    {
        private readonly GameSettings _gameSettings;

        private Tile[,]? _tiles;
        public Tile[,]? GetTiles()
        {
            return _tiles != null ? (Tile[,])_tiles.Clone() : null;
        }

        private Turtle? _turtle;
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

        public BoardController(GameSettings gameSettings)
        {
            _gameSettings = gameSettings ?? throw new System.ArgumentNullException(nameof(gameSettings));

            Init();
        }

        private void Init()
        {
            CreateTilesMatrix();

            SetupExit();

            SetupMines();

            SetupTurtle();
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

        #region Create/Setup Methods
        private void CreateTilesMatrix()
        {
            _tiles = new Tile[_gameSettings.BoardSize.N, _gameSettings.BoardSize.M];
        }

        private void SetupExit()
        {
            int x = _gameSettings.ExitPoint.X ?? 0;
            int y = _gameSettings.ExitPoint.Y ?? 0;

            if (!IsInsideBoard(x, y))
            {
                throw new IndexOutOfBoardException(nameof(GameSettings.ExitPoint), x, y);
            }

            if (!IsEmptySpace(x, y))
            {
                throw new IndexOcupiedException(nameof(GameSettings.ExitPoint), x, y);
            }

            if (_tiles != null)
            {
                _tiles[x, y] = new ExitTile();
            }
        }

        private void SetupMines()
        {
            for (int index = 0; index < _gameSettings.Mines.Count; index++)
            {
                Point mine = _gameSettings.Mines[index];
                string mineDescription = $"{nameof(GameSettings.Mines)}[{index}]";

                AddMine(mine, mineDescription);
            }
        }

        private void AddMine(Point mine, string mineDescription)
        {
            if (_tiles == null)
            {
                return;
            }

            int x = mine.X ?? 0;
            int y = mine.Y ?? 0;

            if (!IsInsideBoard(x, y))
            {
                throw new IndexOutOfBoardException(mineDescription, x, y);
            }

            if (!IsEmptySpace(x, y))
            {
                throw new IndexOcupiedException(mineDescription, x, y);
            }

            _tiles[x, y] = new MineTile();
        }

        private void SetupTurtle()
        {
            var x = _gameSettings.StartingPosition.X ?? 0;
            var y = _gameSettings.StartingPosition.Y ?? 0;

            if (!IsInsideBoard(x, y))
            {
                throw new IndexOutOfBoardException(nameof(GameSettings.StartingPosition), x, y);
            }

            if (!IsEmptySpace(x, y))
            {
                throw new IndexOcupiedException(nameof(GameSettings.StartingPosition), x, y);
            }

            var direction = _gameSettings.StartingPosition.Direction ?? Directions.North;

            _turtle = new Turtle(x, y, direction);
        }
        #endregion

        #region State methods
        private bool IsInsideBoard(int x, int y)
        {
            return _tiles != null && x >= 0 && x < _tiles.GetLength(0) && y >= 0 && y < _tiles.GetLength(1);
        }

        private bool IsEmptySpace(int x, int y)
        {
            return _tiles != null && _tiles[x, y] == null;
        }

        private bool IsOverMine(int x, int y)
        {
            return _tiles != null && _tiles[x, y] is MineTile;
        }

        private bool IsOverExit(int x, int y)
        {
            return _tiles != null && _tiles[x, y] is ExitTile;
        }
        #endregion
    }
}
