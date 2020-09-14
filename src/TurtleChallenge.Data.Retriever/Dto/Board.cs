
using System.ComponentModel.DataAnnotations;

namespace TurtleChallenge.Data.Retriever.Dto
{
    public class Board
    {
        [Required, Range(2, 1000)]
        public int M { get; set; }

        [Required, Range(2, 1000)]
        public int N { get; set; }
    }
}