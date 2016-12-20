using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day20
{
    public class Program20
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').ToList();

            List<BlacklistRange> blacklist = instructions
                .Select(instruction => new BlacklistRange(instruction))
                .OrderBy(b => b.LowerLimit).ToList();

            long? lowest = null;
            long current = 0;
            long countAllowed = 0;
            foreach (BlacklistRange range in blacklist)
            {
                if (current < range.LowerLimit)
                {
                    lowest = lowest ?? current;
                    countAllowed += range.LowerLimit - current;
                }
                
                if (current < range.UpperLimit)
                {
                    current = range.UpperLimit + 1;
                }
            }

            Console.WriteLine("Part one = {0}", lowest);
            Console.WriteLine("Part two = {0}", countAllowed);
            Console.ReadLine();

        }
    }
}
