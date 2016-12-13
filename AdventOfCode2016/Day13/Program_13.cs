using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    public class Program_13
    {
        public static void Main(string[] args)
        {
            const int input = 1352;

            bool[,] maze = new bool[50, 50];

            CreateMaze(input, maze);

            DisplayMaze(maze);

            Console.WriteLine();
            Console.ReadLine();
        }

        public static void DisplayMaze(bool[,] maze)
        {
            for (int y = 0; y < maze.GetLength(0); y++)
            {
                for (int x = 0; x < maze.GetLength(1); x++)
                {
                    if (x == 31 && y == 39)
                    {
                        Console.Write("@");
                    }
                    else
                    {
                        Console.Write(maze[y, x] ? "." : "#");
                    }

                    Console.Write(",");
                }
                Console.WriteLine();
            }
        }

        public static void CreateMaze(int input, bool[,] maze)
        {
            for (int y = 0; y < maze.GetLength(0); y++)
            {
                for (int x = 0; x < maze.GetLength(1); x++)
                {
                    int equation = x * x + 3 * x + 2 * x * y + y + y * y;
                    string res = Convert.ToString(equation + input, 2);
                    int count = res.Length - res.Replace("1", "").Length;
                    maze[y, x] = count % 2 == 0;
                }
            }
        }
    }




}
