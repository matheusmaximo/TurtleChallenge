using TurtleChallenge.Core.Exceptions;
using TurtleChallenge.Data.Retriever.Dto;

namespace TurtleChallenge.Core.Controllers.BoardParts
{
    public class TileMatrix
    {
        private readonly Tile[,] _tiles;

        public Tile[,] GetTiles()
        {
            return (Tile[,])_tiles.Clone();
        }

        public TileMatrix(int n, int m)
        {
            _tiles = new Tile[n, m];
        }

        public void AddTile(Tile tile, Position tilePosition, string tileDescription)
        {
            int x = tilePosition.X;
            int y = tilePosition.Y;

            if (!IsInsideBoard(x, y))
            {
                throw new IndexOutOfBoardException(tileDescription, x, y);
            }

            if (!IsEmptySpace(x, y))
            {
                throw new IndexOcupiedException(tileDescription, x, y);
            }

            _tiles[x, y] = tile;
        }

        public bool IsInsideBoard(int x, int y)
        {
            return x >= 0 && x < _tiles.GetLength(0) && y >= 0 && y < _tiles.GetLength(1);
        }

        public bool IsEmptySpace(int x, int y)
        {
            return _tiles[x, y] == null;
        }

        public bool IsOverMine(int x, int y)
        {
            return _tiles[x, y] is MineTile;
        }

        public bool IsOverExit(int x, int y)
        {
            return _tiles[x, y] is ExitTile;
        }
    }
}
