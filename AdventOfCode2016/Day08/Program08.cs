using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Day08
{
    public class Program08
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').ToList();

            Console.CursorVisible = false;

            int screenHeight = 6;
            int screenWidth = 50;

            bool[,] screen = new bool[screenHeight, screenWidth];

            foreach (string instruction in instructions)
            {
                if (instruction.StartsWith("rect"))
                {
                    string[] parts = instruction.Split(' ')[1].Split('x');
                    int x = int.Parse(parts[1]);
                    int y = int.Parse(parts[0]);

                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < y; j++)
                        {
                            screen[i, j] = true;
                            DisplayAndGetValue(screen);
                        }
                    }
                }
                else if (instruction.StartsWith("rotate column"))
                {
                    string[] parts = instruction.Split(' ');
                    int x = int.Parse(parts[2].Split('=')[1]);
                    int count = int.Parse(parts[4]);

                    bool[] column = screen.SliceColumn(x).ToArray();

                    for (int i = 0; i < column.Count(); i++)
                    {
                        int shift = count % screenHeight;

                        int shiftedIndex = i + shift;

                        if (shiftedIndex >= screenHeight)
                        {
                            shiftedIndex -= screenHeight;
                        }

                        screen[shiftedIndex, x] = column[i];
                        DisplayAndGetValue(screen);

                    }

                }
                else if (instruction.StartsWith("rotate row"))
                {
                    string[] parts = instruction.Split(' ');
                    int y = int.Parse(parts[2].Split('=')[1]);
                    int count = int.Parse(parts[4]);

                    bool[] row = screen.SliceRow(y).ToArray();

                    for (int i = 0; i < row.Count(); i++)
                    {
                        int shift = count % screenWidth;

                        int shiftedIndex = i + shift;

                        if (shiftedIndex >= screenWidth)
                        {
                            shiftedIndex -= screenWidth;
                        }

                        screen[y, shiftedIndex] = row[i];
                        DisplayAndGetValue(screen);

                    }
                }
            }

            int turnedOnPixelsCount = DisplayAndGetValue(screen);

            DisplayAndGetValue(screen);

            Console.WriteLine("Turned on pixels = {0}", turnedOnPixelsCount);

            Console.ReadLine();

        }

        public static int DisplayAndGetValue(bool[,] screen)
        {
            int countOn = 0;

            int screenWidth = screen.GetLength(1);
            int screenHeight = screen.GetLength(0);

            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < screenHeight; i++)
            {
                for (int j = 0; j < screenWidth; j++)
                {

                    if (screen[i, j])
                    {
                        countOn++;
                        Console.Write("█");
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                }
                Console.WriteLine();
            }

            Thread.Sleep(1);

            return countOn;
        }

    }


    public static class ArrayExtensions
    {
        public static IEnumerable<T> SliceRow<T>(this T[,] array, int row)
        {
            for (int i = array.GetLowerBound(1); i <= array.GetUpperBound(1); i++)
            {
                yield return array[row, i];
            }
        }

        public static IEnumerable<T> SliceColumn<T>(this T[,] array, int column)
        {
            for (int i = array.GetLowerBound(0); i <= array.GetUpperBound(0); i++)
            {
                yield return array[i, column];
            }
        }
    }
}
