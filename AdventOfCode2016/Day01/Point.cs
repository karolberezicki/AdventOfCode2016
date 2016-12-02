using System;

namespace Day01
{

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int DistanceFromStart
        {
            get { return Math.Abs(X) + Math.Abs(Y); }
        }

        public Point()
        {

        }

        public Point(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public override bool Equals(object obj)
        {
            Point point = obj as Point;

            return point.X == X && point.Y == Y;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + X.GetHashCode();
            hash = (hash * 7) + Y.GetHashCode();
            return hash;
        }
    }
}
