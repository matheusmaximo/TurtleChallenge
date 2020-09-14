using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TurtleChallenge.Data.Retriever.Dto
{
    public class GameSettings
    {
        [Required]
        public Board BoardSize { get; set; } = new Board();

        [Required]
        public StartingPosition StartingPosition { get; set; } = new StartingPosition();

        [Required]
        public Point ExitPoint { get; set; } = new Point();

        [MaxLength(100)]
        public IList<Point> Mines { get; set; } = new List<Point>();
    }
}