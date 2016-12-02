using System;
using System.Collections.Generic;

namespace Day01
{

    public class Elves
    {
        public Point CurrentLocation { get; set; }

        public Direction CurrentDirection { get; set; }

        public Dictionary<Point, int> VisitedPoints { get; set; }

        public Elves()
        {
            CurrentLocation = new Point { X = 0, Y = 0 };
            CurrentDirection = Direction.North;
            VisitedPoints = new Dictionary<Point, int>();
        }

        public void Move(int moveLength)
        {
            if (CurrentDirection == Direction.North || CurrentDirection == Direction.South)
            {
                int changeXSign = CurrentDirection == Direction.North ? 1 : -1;

                for (int i = 0; i < moveLength; i++)
                {
                    CurrentLocation.X += changeXSign;
                    AddVisitedPoint(new Point(CurrentLocation));
                }
            }
            else
            {
                int changeYSign = CurrentDirection == Direction.East ? 1 : -1;

                for (int i = 0; i < moveLength; i++)
                {
                    CurrentLocation.Y += changeYSign;
                    AddVisitedPoint(new Point(CurrentLocation));
                }
            }

        }

        public void ChangeDirection(char turn)
        {
            int newDirection = (int)CurrentDirection;

            if (CurrentDirection == Direction.North && turn == 'L')
            {
                newDirection = (int)Direction.West;
            }
            else if (turn == 'L')
            {
                newDirection--;
            }
            else
            {
                newDirection++;
            }

            CurrentDirection = (Direction)Math.Abs(newDirection % 4);
        }

        public void AddVisitedPoint(Point point)
        {
            if (VisitedPoints.ContainsKey(point))
            {
                VisitedPoints[point] = VisitedPoints[point] + 1;
            }
            else
            {
                VisitedPoints[point] = 1;
            }
        }
    }
}
