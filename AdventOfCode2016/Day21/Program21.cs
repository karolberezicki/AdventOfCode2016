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

            Console.WriteLine("Part one = {0}", partOne);
            Console.WriteLine("Part two = {0}", "");
            Console.ReadLine();

        }

        private static string Scramble(List<string> instructions, string input)
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


    public static class StringExtensions
    {

        public static string SwapPostition(this string str, int position, int withPosition)
        {
            return str.SwapLetter(str[position], str[withPosition]);
        }

        public static string SwapLetter(this string str, char letter, char withLetter)
        {
            str = str.Replace(letter, '@');
            str = str.Replace(withLetter, letter);
            str = str.Replace('@', withLetter);

            return str;
        }

        public static string RotateRight(this string str, int count)
        {
            if (count >= str.Length)
            {
                count = count - str.Length;
            }

            return str.Substring(str.Length - count, count) + str.Substring(0, str.Length - count);
        }

        public static string RotateLeft(this string str, int count)
        {
            if (count >= str.Length)
            {
                count = count - str.Length;
            }

            return str.Substring(count, str.Length - count) + str.Substring(0, count);
        }

        public static string RotateByLetter(this string str, char letter)
        {
            int count = str.IndexOf(letter);

            if (count >= 4)
            {
                count++;
            }

            return str.RotateRight(count + 1);
        }

        public static string ReversePositions(this string str, int position, int withPosition)
        {
            return str.Substring(0, position) + new string(str.Substring(position, withPosition - position + 1).Reverse().ToArray()) + str.Substring(withPosition + 1, str.Length - withPosition - 1);
        }

        public static string Move(this string str, int position, int toPosition)
        {
            string letter = str[position].ToString();
            str = str.Replace(letter, "");
            return str.Insert(toPosition, letter);
        }
    }

}
