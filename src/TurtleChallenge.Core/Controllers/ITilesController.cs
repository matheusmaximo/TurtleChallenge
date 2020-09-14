using TurtleChallenge.Core.Controllers.BoardParts;

namespace TurtleChallenge.Core.Controllers
{
    public interface ITilesController
    {
        Tile? GetTile(int x, int y);
    }
}