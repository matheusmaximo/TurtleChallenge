using TurtleChallenge.Core.Controllers.BoardParts;
using TurtleChallenge.Core.Exceptions;
using TurtleChallenge.Data.Reporter.Dto;
using TurtleChallenge.Data.Retriever.Dto;

namespace TurtleChallenge.Core.Controllers
{
    public class BoardController : IBoardController
    {
        private readonly GameSettings _gameSettings;

        private Tile[,] _tiles;
        public Tile[,] GetTiles()
        {
            return (Tile[,])_tiles.Clone();
        }

        private Turtle _turtle;
        public Turtle GetTurtle()
        {
            var position = _turtle.GetPosition();
            var direction = _turtle.Direction;
            return new Turtle((int)position.X, (int)position.Y, direction);
        }

        public BoardController(GameSettings gameSettings)
        {
            _gameSettings = gameSettings ?? throw new System.ArgumentNullException(nameof(gameSettings));
        }

        public void Init()
        {
            CreateTilesMatrix();

            SetupExit();

            SetupMines();

            SetupTurtle();
        }

        public void Reset()
        {
            int x = (int)_gameSettings.StartingPosition.X;
            int y = (int)_gameSettings.StartingPosition.Y;
            var direction = (Directions)_gameSettings.StartingPosition.Direction;

            _turtle = new Turtle(x, y, direction);
        }

        public void MoveTurtle()
        {
            _turtle.Move();

            var position = _turtle.GetPosition();

            int x = (int)position.X;
            int y = (int)position.Y;

            if (!IsInsideBoard(x, y))
            {
                throw new IndexOutOfBoardException(nameof(Turtle), x, y);
            }

            if (IsOverMine(x, y))
            {
                throw new TurtleHitMineException();
            }
        }

        public MovesResultPossibilities GetResult()
        {
            var position = _turtle.GetPosition();

            int x = (int)position.X;
            int y = (int)position.Y;

            return IsOverExit(x, y) ? MovesResultPossibilities.Success : MovesResultPossibilities.InDanger;
        }

        public void RotateTurtle()
        {
            _turtle.Rotate();
        }

        #region Create/Setup Methods
        private void CreateTilesMatrix()
        {
            _tiles = new Tile[(int)_gameSettings.BoardSize.N, (int)_gameSettings.BoardSize.M];
        }

        private void SetupExit()
        {
            int x = (int)_gameSettings.ExitPoint.X;
            int y = (int)_gameSettings.ExitPoint.Y;

            if (!IsInsideBoard(x, y))
            {
                throw new IndexOutOfBoardException(nameof(GameSettings.ExitPoint), x, y);
            }

            if (!IsEmptySpace(x, y))
            {
                throw new IndexOcupiedException(nameof(GameSettings.ExitPoint), x, y);
            }

            _tiles[x, y] = new ExitTile();
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
            int x = (int)mine.X;
            int y = (int)mine.Y;

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
            var x = (int)_gameSettings.StartingPosition.X;
            var y = (int)_gameSettings.StartingPosition.Y;

            if (!IsInsideBoard(x, y))
            {
                throw new IndexOutOfBoardException(nameof(GameSettings.StartingPosition), x, y);
            }

            if (!IsEmptySpace(x, y))
            {
                throw new IndexOcupiedException(nameof(GameSettings.StartingPosition), x, y);
            }

            var direction = (Directions)_gameSettings.StartingPosition.Direction;

            _turtle = new Turtle(x, y, direction);
        }
        #endregion

        #region State methods
        private bool IsInsideBoard(int x, int y)
        {
            return x >= 0 && x < _tiles.GetLength(0) && y >= 0 && y < _tiles.GetLength(1);
        }

        private bool IsEmptySpace(int x, int y)
        {
            return _tiles[x, y] == null;
        }

        private bool IsOverMine(int x, int y)
        {
            return _tiles[x, y] is MineTile;
        }

        private bool IsOverExit(int x, int y)
        {
            return _tiles[x, y] is ExitTile;
        }
        #endregion
    }
}
