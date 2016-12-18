using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day13
{
    public class Program13
    {
        public static void Main(string[] args)
        {
            const int input = 1352;

            //bool[,] maze = new bool[50, 50];
            //CreateMaze(input, maze);
            //DisplayMaze(maze);

            Queue<MazeState> statesQueue = new Queue<MazeState>(new[] { new MazeState(1, 1, 0) });
            MazeState finishState = null;

            HashSet<Point> placesInRangeOf50 = new HashSet<Point>();

            Point target = new Point(31, 39);

            while (statesQueue.Count > 0)
            {
                MazeState currentState = statesQueue.Dequeue();

                if (currentState.Distance <= 50 && !placesInRangeOf50.Contains(currentState.Position))
                {
                    placesInRangeOf50.Add(currentState.Position);
                }

                if (currentState.Position == target)
                {
                    finishState = finishState ?? currentState;
                }
                else
                {
                    HashSet<MazeState> possibleMoves = new HashSet<MazeState>
                    {
                        new MazeState(currentState.Position.X + 1, currentState.Position.Y, currentState.Distance + 1),
                        new MazeState(currentState.Position.X - 1, currentState.Position.Y, currentState.Distance + 1),
                        new MazeState(currentState.Position.X, currentState.Position.Y + 1, currentState.Distance + 1),
                        new MazeState(currentState.Position.X, currentState.Position.Y - 1, currentState.Distance + 1)
                    };

                    if (finishState != null)
                    {
                        possibleMoves =
                            new HashSet<MazeState>(possibleMoves.Where(move => move.Distance < finishState.Distance));
                    }

                    foreach (MazeState newState in possibleMoves)
                    {
                        if (IsOpenSpace(input, newState.Position.Y, newState.Position.X) && !statesQueue.Contains(newState))
                        {
                            statesQueue.Enqueue(newState);
                        }
                    }

                }
            }


            int partOne = finishState?.Distance ?? 0;
            int partTwo = placesInRangeOf50.Count;

            Console.WriteLine("Part one = {0}", partOne);
            Console.WriteLine("Part two = {0}", partTwo);
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
                    maze[y, x] = IsOpenSpace(input, y, x);
                }
            }
        }

        private static bool IsOpenSpace(int input, int y, int x)
        {
            if (x < 0 || y < 0)
            {
                return false;
            }

            int equation = x * x + 3 * x + 2 * x * y + y + y * y;
            string res = Convert.ToString(equation + input, 2);
            int count = res.Length - res.Replace("1", "").Length;
            return count % 2 == 0;
        }
    }
}
