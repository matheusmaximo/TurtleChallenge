using System.ComponentModel;

namespace TurtleChallenge.Data.Reporter.Dto
{
    public enum MovesResultPossibilities
    {
        [Description("Still in danger")]
        InDanger,

        [Description("Mine hit")]
        MineHit,

        [Description("Out of board")]
        OutOfBoard,

        [Description("Success")]
        Success
    }
}
