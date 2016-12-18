using System.Diagnostics;

namespace Day13
{
    [DebuggerDisplay("X = {X}, Y = {Y},  Distance = {Distance}")]

    public class MazeState
    {
        public MazeState(int x, int y, int distance)
        {
            X = x;
            Y = y;
            Distance = distance;
        }

        public int X { get; }
        public int Y { get; }
        public int Distance { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            MazeState mazeState = (MazeState)obj;
            return (X == mazeState.X) && (Y == mazeState.Y);
        }
        public override int GetHashCode()
        {
            return X ^ Y;
        }
    }
}