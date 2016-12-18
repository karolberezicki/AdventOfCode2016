using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03
{
    public class Program03
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').ToList();

            int countTrianglesPartOne = instructions
                .Select(SplitPoints)
                .Select(t => new Triangle(t))
                .Count(t => t.IsValidTriangle);

            int countTrianglesPartTwo = 0;
            for (int i = 0; i < instructions.Count; i = i + 3)
            {
                List<string> lineOne = SplitPoints(instructions[i]);
                List<string> lineTwo = SplitPoints(instructions[i + 1]);
                List<string> lineThree = SplitPoints(instructions[i + 2]);

                List<Triangle> triangles = new List<Triangle>
                {
                     new Triangle(lineOne[0], lineTwo[0], lineThree[0]),
                     new Triangle(lineOne[1], lineTwo[1], lineThree[1]),
                     new Triangle(lineOne[2], lineTwo[2], lineThree[2]),
                };

                countTrianglesPartTwo += triangles.Count(t => t.IsValidTriangle);
            }

            Console.WriteLine("Part one = {0}", countTrianglesPartOne);
            Console.WriteLine("Part two = {0}", countTrianglesPartTwo);
            Console.ReadLine();

        }

        public static List<string> SplitPoints(string instruction)
        {
            return instruction.Split(' ').Where(dd => !string.IsNullOrWhiteSpace(dd)).ToList();
        }
    }
}
