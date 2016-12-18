using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    public partial class Program10
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').ToList();

            
            List<Bot> bots = instructions
                .Where(i => i.StartsWith("bot"))
                .Select(i => new Bot(i))
                .ToList();

            foreach (string instruction in instructions)
            {
                if (instruction.StartsWith("value"))
                {
                    string[] parts = instruction.Split(' ');
                    int value = int.Parse(parts[1]);
                    int botNumber = int.Parse(parts[5]);

                    bots.First(b => b.Number == botNumber).Values.Add(value);

                }
            }

            while (bots.Count(b => b.Values.Count == 2) != bots.Count)
            {
                // Needed to not overwrite values
                List<Bot> botsWithTwoValues = bots.Where(b => b.Values.Count == 2).ToList();

                foreach (Bot bot in botsWithTwoValues)
                {
                    if (!bot.GivesHighToOutput)
                    {
                        if (bots.First(b => b.Number == bot.GiveHighValueTo).Values.Count < 2)
                        {
                            bots.First(b => b.Number == bot.GiveHighValueTo)
                                .Values.Add(bot.Values.Max());
                        }

                    }

                    if (!bot.GivesLowToOutput)
                    {
                        if (bots.First(b => b.Number == bot.GiveLowValueTo).Values.Count < 2)
                        {
                            bots.First(b => b.Number == bot.GiveLowValueTo)
                                .Values.Add(bot.Values.Min());
                        }
                    }
                }
            }

            int botNumberWith61And17Microchips =
                bots.First(b => b.Values.Contains(61) && b.Values.Contains(17)).Number;

            List<Output> outputs = bots.Where(b => b.GivesHighToOutput || b.GivesLowToOutput)
                .Select(b =>
                b.GivesLowToOutput
                ? new Output(b.GiveLowValueTo, b.Values.Min())
                : new Output(b.GiveHighValueTo, b.Values.Max())).ToList();

            int multiplyOutputs0And1And2 =
                outputs.First(o => o.Number == 0).Value
                * outputs.First(o => o.Number == 1).Value
                * outputs.First(o => o.Number == 2).Value;

            Console.WriteLine("Part one = {0}", botNumberWith61And17Microchips);
            Console.WriteLine("Part two = {0}", multiplyOutputs0And1And2);
            Console.ReadLine();

        }
    }
}
