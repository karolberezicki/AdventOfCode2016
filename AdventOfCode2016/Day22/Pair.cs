using System.Drawing;

namespace Day22
{
    public class Pair
    {
        public Pair(Point a, Point b)
        {
            PointA = a;
            PointB = b;
        }

        public Point PointA { get; }
        public Point PointB { get; }

        public override bool Equals(object obj)
        {
            Pair item = obj as Pair;
            return item != null 
                   && (PointA.Equals(item.PointA) && PointB.Equals(item.PointB)
                       || PointB.Equals(item.PointA) && PointA.Equals(item.PointB));
        }

        public override int GetHashCode()
        {
            return PointA.GetHashCode() ^ PointB.GetHashCode();
        }
    }
}