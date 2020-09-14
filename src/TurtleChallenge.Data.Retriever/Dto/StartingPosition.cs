using System.ComponentModel.DataAnnotations;

namespace TurtleChallenge.Data.Retriever.Dto
{
    public class StartingPosition : Point
    {
        [Required]
        public Directions? Direction { get; set; }
    }
}