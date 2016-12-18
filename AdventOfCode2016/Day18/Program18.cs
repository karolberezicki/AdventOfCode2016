using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day18
{
    public class Program18
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);

            int partOne = CountSafeTiles(source, 40);
            int partTwo = CountSafeTiles(source, 400000);

            Console.WriteLine("Part one = {0}", partOne);
            Console.WriteLine("Part two = {0}", partTwo);
            Console.ReadLine();

        }

        public static int CountSafeTiles(string source, int rowsCount)
        {
            const char safeTile = '.';
            const char trap = '^';

            string currentRow = source;
            List<string> rows = new List<string> { currentRow };

            while (rows.Count < rowsCount)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < currentRow.Length; i++)
                {
                    char left = i == 0 ? safeTile : currentRow[i - 1];
                    char right = i == currentRow.Length - 1 ? safeTile : currentRow[i + 1];

                    sb.Append(GetNewTile(safeTile, trap, left, right));
                }

                currentRow = sb.ToString();
                rows.Add(currentRow);

            }

            int safeTilesCount = string.Join("", rows).Count(tile => tile == safeTile);
            return safeTilesCount;
        }

        private static char GetNewTile(char safeTile, char trap, char left, char right)
        {
            return (left == trap && right == safeTile) || (right == trap && left == safeTile) ? trap : safeTile;
        }
    }
}
