using System.ComponentModel.DataAnnotations;

namespace TurtleChallenge.Data.Retriever.Dto
{
    public class Point
    {
        [Required, Range(0, 999)]
        public int? X { get; set; }

        [Required, Range(0, 999)]
        public int? Y { get; set; }
    }
}