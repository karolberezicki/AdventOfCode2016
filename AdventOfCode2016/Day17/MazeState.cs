using System.Diagnostics;

namespace Day17
{
    [DebuggerDisplay("X = {X}, Y = {Y},  MazePath = {MazePath}")]

    public class MazeState
    {
        public MazeState(int x, int y, string mazePath)
        {
            X = x;
            Y = y;
            MazePath = mazePath;
        }

        public int X { get; }
        public int Y { get; }
        public string MazePath { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            MazeState mazeState = (MazeState)obj;
            return
                (X == mazeState.X) && (Y == mazeState.Y) &&
                (MazePath == mazeState.MazePath);
        }
        public override int GetHashCode()
        {
            return
                X* 389 ^ Y* 853 ^
                MazePath.GetHashCode() * 257;
        }
    }
}