using TurtleChallenge.Core.Controllers;
using TurtleChallenge.Data.Retriever.Dto;
using TurtleChallenge.Tests.Utils.xUnit.Settings;
using Xunit;

namespace TurtleChallenge.Core.Tests.Controllers
{
    [Trait(Traits.Category, Categories.Unit)]
    public class TurtleTests
    {
        [Fact]
        public void GetPosition_GivenCoordinates_ShouldReturnPoint()
        {
            int x = 5;
            int y = 10;
            var direction = Directions.East;

            var sut = new Turtle(x, y, direction);
            var result = sut.GetPosition();

            Assert.Equal(x, result.X);
            Assert.Equal(y, result.Y);
        }

        [Fact]
        public void Move_GivenCoordinatesAndDirectionNorth_ShouldIncreamentY()
        {
            int x = 5;
            int y = 10;
            var direction = Directions.North;

            var sut = new Turtle(x, y, direction);
            sut.Move();
            var result = sut.GetPosition();

            Assert.Equal(x, result.X);
            Assert.Equal(y + 1, result.Y);
        }

        [Fact]
        public void Move_GivenCoordinatesAndDirectionEast_ShouldIncreamentX()
        {
            int x = 5;
            int y = 10;
            var direction = Directions.East;

            var sut = new Turtle(x, y, direction);
            sut.Move();
            var result = sut.GetPosition();

            Assert.Equal(x + 1, result.X);
            Assert.Equal(y, result.Y);
        }

        [Fact]
        public void Move_GivenCoordinatesAndDirectionSouth_ShouldDecrementY()
        {
            int x = 5;
            int y = 10;
            var direction = Directions.South;

            var sut = new Turtle(x, y, direction);
            sut.Move();
            var result = sut.GetPosition();

            Assert.Equal(x, result.X);
            Assert.Equal(y - 1, result.Y);
        }

        [Fact]
        public void Move_GivenCoordinatesAndDirectionWest_ShouldDecrementX()
        {
            int x = 5;
            int y = 10;
            var direction = Directions.West;

            var sut = new Turtle(x, y, direction);
            sut.Move();
            var result = sut.GetPosition();

            Assert.Equal(x - 1, result.X);
            Assert.Equal(y, result.Y);
        }

        [Fact]
        public void Rotate_GivenCoordinatesAndDirection_ShouldChangeDirection()
        {
            int x = 5;
            int y = 10;
            var direction = Directions.North;
            var nextDirection = Directions.East;

            var sut = new Turtle(x, y, direction);
            sut.Rotate();
            var result = sut.Direction;

            Assert.Equal(nextDirection, result);
        }

        [Fact]
        public void Rotate_GivenCoordinatesAndDirectionWest_ShouldChangeDirectionToNorth()
        {
            int x = 5;
            int y = 10;
            var direction = Directions.West;
            var nextDirection = Directions.North;

            var sut = new Turtle(x, y, direction);
            sut.Rotate();
            var result = sut.Direction;

            Assert.Equal(nextDirection, result);
        }

        [Fact]
        public void RotateAndMove_GivenCoordinatesAndDirection_ShouldMoveToNewDirection()
        {
            int x = 5;
            int y = 10;
            var direction = Directions.West;

            var sut = new Turtle(x, y, direction);
            sut.Rotate();
            sut.Move();
            var result = sut.GetPosition();

            Assert.Equal(x, result.X);
            Assert.Equal(y + 1, result.Y);
        }
    }
}
