using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day21
{
    public class Program21
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').ToList();

            string partOne = Scramble(instructions, "abcdefgh");

            List<char> baseList = "abcdefgh".ToCharArray().ToList();
            IEnumerable<string> permutations = Permutations.GeneratePermutations(baseList)
                .Select(a => new string(a.ToArray()));

            const string scrambled = "fbgdceah";
            string unscrambled = "";

            foreach (string permutation in permutations)
            {

                string currentScrambled = Scramble(instructions, permutation);

                if (currentScrambled != scrambled)
                {
                    continue;
                }

                unscrambled = permutation;
                break;
            }

            Console.WriteLine("Part one = {0}", partOne);
            Console.WriteLine("Part two = {0}", unscrambled);
            Console.ReadLine();

        }

        private static string Scramble(IEnumerable<string> instructions, string input)
        {
            foreach (string instruction in instructions)
            {
                string[] parts = instruction.Split(' ');

                if (instruction.Contains("swap position"))
                {
                    int x = int.Parse(parts[2]);
                    int y = int.Parse(parts[5]);
                    input = input.SwapPostition(x, y);

                }

                if (instruction.Contains("swap letter"))
                {
                    char x = parts[2].First();
                    char y = parts[5].First();
                    input = input.SwapLetter(x, y);
                }

                if (instruction.Contains("rotate left"))
                {
                    int x = int.Parse(parts[2]);
                    input = input.RotateLeft(x);
                }

                if (instruction.Contains("rotate right"))
                {
                    int x = int.Parse(parts[2]);
                    input = input.RotateRight(x);
                }

                if (instruction.Contains("rotate based on position"))
                {
                    char x = parts[6].First();
                    input = input.RotateByLetter(x);
                }

                if (instruction.Contains("reverse positions"))
                {
                    int x = int.Parse(parts[2]);
                    int y = int.Parse(parts[4]);
                    input = input.ReversePositions(x, y);

                }

                if (instruction.Contains("move position"))
                {
                    int x = int.Parse(parts[2]);
                    int y = int.Parse(parts[5]);
                    input = input.Move(x, y);

                }

            }

            return input;
        }
    }
}
