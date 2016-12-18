using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
{
    public class Program11
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').ToList();


            List<List<string>> floors = new List<List<string>>();

            foreach (string instruction in instructions)
            {
                List<string> floor = new List<string>();

                string[] parts = instruction.Replace(",", "").Replace(".", "").Split(' ');

                for (int i = 0; i < parts.Length; i++)
                {
                    if (parts[i] != "generator" && parts[i] != "microchip")
                    {
                        continue;
                    }

                    string machinePart = ("" + parts[i - 1][0] + parts[i - 1][1] + parts[i][0]).ToUpper();
                    floor.Add(machinePart);
                }

                floors.Add(floor);

            }

            DisplayFloors(floors);
            Console.ReadLine();
        }

        private static void DisplayFloors(IList<List<string>> floors)
        {
            foreach (List<string> list in floors)
            {
                Console.Write("Floor {0}: ", floors.IndexOf(list) + 1);
                Console.WriteLine(string.Join(", ", list));
            }
        }
    }
}
