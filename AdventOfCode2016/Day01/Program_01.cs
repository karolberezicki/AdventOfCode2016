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

            List<string> list = source.Split(',').ToList();

            var currentOrientation = Orientation.North;
            int X = 0;
            int Y = 0;

            int[] adfdsfds = new int[1000];


            foreach (var item in list)
            {

                var moveLength = int.Parse(new string(item.Where(c => char.IsDigit(c)).ToArray()));

                var changeOrientation = currentOrientation.ToString() + item[0];

                switch (changeOrientation)
                {
                    case "NorthR":
                        currentOrientation = Orientation.East;
                        Y += moveLength;
                        break;
                    case "NorthL":
                        currentOrientation = Orientation.West;
                        Y -= moveLength;
                        break;
                    case "EastR":
                        currentOrientation = Orientation.South;
                        X -= moveLength;
                        break;
                    case "EastL":
                        currentOrientation = Orientation.North;
                        X += moveLength;
                        break;
                    case "SouthR":
                        currentOrientation = Orientation.West;
                        Y -= moveLength;
                        break;
                    case "SouthL":
                        currentOrientation = Orientation.East;
                        Y += moveLength;
                        break;
                    case "WestR":
                        currentOrientation = Orientation.North;
                        X += moveLength;
                        break;
                    case "WestL":
                        currentOrientation = Orientation.South;
                        X -= moveLength;
                        break;
                    default:
                        break;
                }

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
