using System;
using System.Collections.Generic;
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

            int goalX = 31;
            int goalY = 39;
            
            // Add start position
            HashSet<MazeState> mazeStates = new HashSet<MazeState>{ new MazeState(1, 1, 0) };

            FindPossibleStates(mazeStates, input, goalX, goalY);

            int minimalDistance = mazeStates.First(s => s.X == goalX && s.Y == goalY).Distance;

            int placesInRangeOf50 = mazeStates.Count(s => s.Distance <= 50);

            Console.WriteLine("Part one = {0}", minimalDistance);
            Console.WriteLine("Part two = {0}", placesInRangeOf50);
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
            int equation = x * x + 3 * x + 2 * x * y + y + y * y;
            string res = Convert.ToString(equation + input, 2);
            int count = res.Length - res.Replace("1", "").Length;
            return count % 2 == 0;
        }

        private static void FindPossibleStates(ISet<MazeState> mazeStates, int input, int goalX, int goalY)
        {
            if (mazeStates.FirstOrDefault(s => s.X == goalX && s.Y == goalY) != null)
            {
                return;
            }

            HashSet<MazeState> statesToAdd = new HashSet<MazeState>();

            foreach (MazeState mazeState in mazeStates)
            {
                HashSet<MazeState> possibleMoves = new HashSet<MazeState>
                {
                    new MazeState(mazeState.X + 1, mazeState.Y, mazeState.Distance + 1),
                    new MazeState(mazeState.X - 1, mazeState.Y, mazeState.Distance + 1),
                    new MazeState(mazeState.X, mazeState.Y + 1, mazeState.Distance + 1),
                    new MazeState(mazeState.X, mazeState.Y - 1, mazeState.Distance + 1)
                };

                foreach (MazeState newState in possibleMoves)
                {
                    if (IsOpenSpace(input, newState.Y, newState.X) 
                        && newState.X >= 0 
                        && newState.Y >= 0 
                        && !mazeStates.Contains(newState))
                    {
                        statesToAdd.Add(newState);
                    }
                }
            }

            mazeStates.UnionWith(statesToAdd);

            if (statesToAdd.Count > 0)
            {
                FindPossibleStates(mazeStates, input, goalX, goalY);
            }
        }

    }
}
