using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    public class Program19
    {
        public static void Main(string[] args)
        {
            const int input = 3001330;

            int partOne = Part1(input);

            Console.WriteLine("Part one = {0}", partOne);
            //Console.WriteLine("Part two = {0}", partTwo);
            Console.ReadLine();
        }

        private static int Part1(int input)
        {
            List<Elf> elves = new List<Elf>();
            for (int i = 0; i < input; i++)
            {

                elves.Add(new Elf { Number = i + 1, Presents = 1 });

            }


            while (elves.Count > 1)
            {
                for (int i = 0; i < elves.Count; i++)
                {

                    if (elves[i].Presents == 0)
                    {
                        continue;
                    }

                    if (i == elves.Count - 1)
                    {
                        elves[i].Presents += elves[0].Presents;
                        elves[0].Presents = 0;
                    }
                    else
                    {
                        elves[i].Presents += elves[i + 1].Presents;
                        elves[i + 1].Presents = 0;
                    }

                }

                elves = elves.Where(e => e.Presents > 0).ToList();

            }

            return elves.First().Number;
        }
    }


    public class Elf
    {
        public int Number { get; set; }
        public int Presents { get; set; }
    }
}
