using Day01;
using Xunit;

namespace UnitTests
{
    public class UnitTests
    {
        [Fact]
        public void AreUnitTestsWorking()
        {
            bool theTruth = true;
            Assert.True(theTruth);
        }

        [Theory]
        [InlineData(Direction.North, 'L', Direction.West)]
        [InlineData(Direction.North, 'R', Direction.East)]
        [InlineData(Direction.East,  'L', Direction.North)]
        [InlineData(Direction.East,  'R', Direction.South)]
        [InlineData(Direction.South, 'L', Direction.East)]
        [InlineData(Direction.South, 'R', Direction.West)]
        [InlineData(Direction.West,  'L', Direction.South)]
        [InlineData(Direction.West,  'R', Direction.North)]
        public void ElvesChangeDirectionTest(Direction currentDirection, char turn, Direction newDirection)
        {
            Elves elves = new Elves { CurrentDirection = currentDirection };
            elves.ChangeDirection(turn);
            Assert.Equal(newDirection, elves.CurrentDirection);
        }

    }
}