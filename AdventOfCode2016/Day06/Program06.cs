using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{
    public class Program06
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').ToList();


            List<Dictionary<char, int>> signal = new List<Dictionary<char, int>>();

            for (int i = 0; i < instructions.First().Length; i++)
            {
                signal.Add(new Dictionary<char, int>());
            }

            foreach (string instruction in instructions)
            {
                for (int i = 0; i < instruction.Length; i++)
                {
                    AddSignal(signal, i, instruction[i]);
                }
            }

            string messagePartOne = "";
            foreach (Dictionary<char, int> item in signal)
            {
                char mostCommonChar = item.ToList().OrderByDescending(x => x.Value).First().Key;
                messagePartOne = messagePartOne + mostCommonChar;
            }

            string messagePartTwo = "";
            foreach (Dictionary<char, int> item in signal)
            {
                char leastCommonChar = item.ToList().OrderBy(x => x.Value).First().Key;
                messagePartTwo = messagePartTwo + leastCommonChar;
            }

            Console.WriteLine("Part one = {0}", messagePartOne);
            Console.WriteLine("Part two = {0}", messagePartTwo);
            Console.ReadLine();

        }

        public static void AddSignal(List<Dictionary<char, int>> signal, int position, char key)
        {

            if (signal[position].ContainsKey(key))
            {
                signal[position][key] = signal[position][key] + 1;
            }
            else
            {
                signal[position].Add(key, 1);
            }
        }
    }
}
