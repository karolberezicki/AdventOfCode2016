using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
{
    public class Program01
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
                elves.ChangeDirection(instruction[0]);
                elves.Move(moveLength);
            }

            Console.WriteLine("Part one = {0}", elves.CurrentLocation.DistanceFromStart);
            Console.WriteLine("Part two = {0}", elves.VisitedPoints.First(a => a.Value >= 2).Key.DistanceFromStart);

            Console.ReadLine();

        }
    }
}
