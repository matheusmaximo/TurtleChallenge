using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TurtleChallenge.Data.Retriever.Dto
{
    public class GameSettings
    {
        private IList<Point> _mines = new Collection<Point>();

        [Required]
        public Board BoardSize { get; set; }

        [Required]
        public StartingPosition StartingPosition { get; set; }

        [Required]
        public Point ExitPoint { get; set; }

        [MaxLength(100)]
        public IList<Point> Mines { get => new Collection<Point>(_mines); set => _mines = value; }
    }
}