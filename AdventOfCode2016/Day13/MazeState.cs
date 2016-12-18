using System.Diagnostics;
using System.Drawing;

namespace Day13
{
    [DebuggerDisplay("X = {Position.X}, Y = {Position.Y},  Distance = {Distance}")]

    public class MazeState
    {
        public MazeState(int x, int y, int distance)
        {
            Position = new Point(x, y); 
            Distance = distance;
        }

        public Point Position { get; }
        public int Distance { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            MazeState mazeState = (MazeState)obj;
            return (Position == mazeState.Position) && (Distance == mazeState.Distance);
        }
        public override int GetHashCode()
        {
            return Position.GetHashCode() ^ Distance.GetHashCode();
        }
    }
}