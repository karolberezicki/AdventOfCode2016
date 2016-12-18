using Day01;
using Day09;
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
        [Theory]
        [InlineData("ADVENT", "ADVENT")]
        [InlineData("A(1x5)BC", "ABBBBBC")]
        [InlineData("(3x3)XYZ", "XYZXYZXYZ")]
        [InlineData("A(2x2)BCD(2x2)EFG", "ABCBCDEFEFG")]
        [InlineData("(6x1)(1x3)A", "(1x3)A")]
        [InlineData("X(8x2)(3x3)ABCY", "X(3x3)ABC(3x3)ABCY")]
        public void DecompressTests(string before, string after)
        {
            string decompressed = Program09.Decompress(before).ToString();
            Assert.Equal(after, decompressed);
        }



    }
}