using System;

namespace Day19
{
    public class Program19
    {
        public static void Main(string[] args)
        {
            const int input = 3001330;

            int partOne = Part1(input);
            int partTwo = Part2(input);

            Console.WriteLine("Part one = {0}", partOne);
            Console.WriteLine("Part two = {0}", partTwo);
            Console.ReadLine();
        }

        private static int Part1(int input)
        {
            string binary = Convert.ToString(input, 2);
            return Convert.ToInt32(binary.Substring(1, binary.Length - 1) + binary[0], 2);
        }

        private static int Part2(int input)
        {
            return input - (int)Math.Pow(3, (int)Math.Log(input, 3));
        }
    }
}
