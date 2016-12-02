using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    public class Program_01
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            source = source.Replace(" ", "");
            List<string> intructions = source.Split(',').ToList();
            
            Elves elves = new Elves();


            foreach (string instruction in intructions)
            {

                int moveLength = int.Parse(new string(instruction.Where(c => char.IsDigit(c)).ToArray()));

                string changeOrientation = elves.Orientation.ToString() + instruction[0];                

                if (changeOrientation == "NorthR" || changeOrientation == "SouthL")
                {
                    elves.Move(Orientation.East, moveLength);
                }
                else if (changeOrientation == "NorthL" || changeOrientation == "SouthR")
                {
                    elves.Move(Orientation.West, moveLength);
                }
                else if (changeOrientation == "EastR" || changeOrientation == "WestL")
                {
                    elves.Move(Orientation.South, moveLength);
                }
                else if (changeOrientation == "EastL" || changeOrientation == "WestR")
                {
                    elves.Move(Orientation.North, moveLength);
                }                

            }

            Console.WriteLine("Part one = {0}", (Math.Abs(elves.X) + Math.Abs(elves.Y)));
            KeyValuePair<string, int> firstPointVisitedTwice = elves.VisitedPoints.First(a => a.Value >= 2);
            string[] firstPointVisitedTwiceValues = firstPointVisitedTwice.Key.Split(',');
            Console.WriteLine("Part two = {0}", (Math.Abs(int.Parse(firstPointVisitedTwiceValues[0])) + Math.Abs(int.Parse(firstPointVisitedTwiceValues[1]))));

            Console.WriteLine(firstPointVisitedTwice.Key);
            Console.ReadLine();

        }
    }

    public class Elves
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Orientation Orientation { get; set; }

        public Dictionary<string,int> VisitedPoints { get; set; }

        public Elves()
        {
            X = 0;
            Y = 0;
            Orientation = Orientation.North;
            VisitedPoints = new Dictionary<string, int>();
                
        }

        public void Move(Orientation newOrientation, int moveLength)
        {
            Orientation = newOrientation;

            if (Orientation == Orientation.North || Orientation == Orientation.South)
            {
                int changeXSign = newOrientation == Orientation.North ? 1 : -1;

                for (int i = 0; i < moveLength; i++)
                {
                    X += changeXSign;
                    AddVisitedPoint(X, Y);
                }
            }
            else
            {
                int changeYSign = newOrientation == Orientation.East ? 1 : -1;

                for (int i = 0; i < moveLength; i++)
                {
                    Y += changeYSign;
                    AddVisitedPoint(X, Y);
                }
            }
            
        }

        public void AddVisitedPoint(int visitedX, int visitedY)
        {
            string key = string.Format("{0},{1}", visitedX, visitedY);
            if (VisitedPoints.ContainsKey(key))
            {
                VisitedPoints[key] = VisitedPoints[key] + 1;
            }
            else
            {
                VisitedPoints[key] = 1;
            }            
        }
    }



    public enum Orientation
    {
        North,
        East,
        South,
        West
    }
}
