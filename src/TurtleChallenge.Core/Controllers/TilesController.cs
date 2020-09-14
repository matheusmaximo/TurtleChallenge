using TurtleChallenge.Core.Controllers.BoardParts;
using TurtleChallenge.Data.Retriever.Dto;

namespace TurtleChallenge.Core.Controllers
{
    public class TilesController : ITilesController
    {
        private readonly TileMatrix _tileMatrix;

        public TilesController(GameSettings gameSettings)
        {
            _tileMatrix = new TileMatrix(gameSettings.BoardSize.N, gameSettings.BoardSize.M);

            Init(gameSettings);
        }

        public Tile? GetTile(int x, int y)
        {
            if(!_tileMatrix.IsInsideBoard(x, y))
            {
                return null;
            }

            return _tileMatrix.GetTiles()[x, y] ?? new Tile();
        }

        private void Init(GameSettings gameSettings)
        {
            SetupExit(gameSettings);

            SetupMines(gameSettings);
        }

        private void SetupExit(GameSettings gameSettings)
        {
            int x = gameSettings.ExitPoint.X;
            int y = gameSettings.ExitPoint.Y;

            var tile = new ExitTile();
            var tilePosition = new Position(x, y);
            var tileDescription = nameof(GameSettings.ExitPoint);

            _tileMatrix.AddTile(tile, tilePosition, tileDescription);
        }

        private void SetupMines(GameSettings gameSettings)
        {
            for (int index = 0; index < gameSettings.Mines.Count; index++)
            {
                Point mine = gameSettings.Mines[index];

                var tile = new MineTile();
                var tilePosition = new Position(mine.X, mine.Y);
                var tileDescription = $"{nameof(GameSettings.Mines)}[{index}]";
                _tileMatrix.AddTile(tile, tilePosition, tileDescription);
            }
        }
    }
}
