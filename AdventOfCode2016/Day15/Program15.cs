using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    public class Program15
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').ToList();

            int partOne = CalculatePerfectTime(instructions);

            int nextDiskNumber = instructions.Count + 1;
            instructions.Add($"Disc #{nextDiskNumber} has 11 positions; at time=0, it is at position 0.");
            int partTwo = CalculatePerfectTime(instructions);

            Console.WriteLine("Part one = {0}", partOne);
            Console.WriteLine("Part two = {0}", partTwo);
            Console.ReadLine();
        }

        public static int CalculatePerfectTime(List<string> instructions)
        {
            List<Disk> disks = PrepareDisks(instructions);

            int time = 0;

            while (!disks.All(d => d.IsAligned))
            {
                foreach (Disk disk in disks)
                {
                    disk.IncrementPosition();
                }
                time++;
            }

            return time - disks.Count;
        }

        private static List<Disk> PrepareDisks(IReadOnlyCollection<string> instructions)
        {
            int totalDisks = instructions.Count;
            return instructions
                .Select(instruction => instruction.Replace(".", "").Replace("#", "").Split(' '))
                .Select(parts => new Disk
                {
                    Number = int.Parse(parts[1]),
                    OrderNumber = totalDisks - int.Parse(parts[1]),
                    PossiblePositions = int.Parse(parts[3]),
                    CurrentPosition = int.Parse(parts[11])
                }).ToList();
        }
    }
}
