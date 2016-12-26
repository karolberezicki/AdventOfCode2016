using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Day24
{
    public class Program24
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').ToList();
            char[,] maze = CreateMaze(instructions);

            List<char> locations = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7' };

            List<Point> points = locations.Select(l => FindPoint(maze, l)).ToList();

            List<List<char>> locationPermutations = Permutations.GeneratePermutations(locations);
            locationPermutations = locationPermutations.Where(lp => lp[0] == '0').ToList();

            Dictionary<string, int> storedDistances = new Dictionary<string, int>();
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = 0; j < points.Count; j++)
                {
                    MazeState state = FindShortestPath(maze, points[i], points[j]);
                    storedDistances.Add(i+""+j, state.Distance);
                }
            }


            List<PathDistance> pathDistances = locationPermutations.Select(lp => new PathDistance{Locations = lp, Distance = GetTotalDistance(storedDistances, lp)}).ToList();

            int partOne = pathDistances.Min().Distance;

            foreach (List<char> locationPermutation in locationPermutations)
            {
                locationPermutation.Add('0');
            }

            pathDistances = locationPermutations.Select(lp => new PathDistance { Locations = lp, Distance = GetTotalDistance(storedDistances, lp) }).ToList();

            int partTwo = pathDistances.Min().Distance;

            Console.WriteLine("Part one = {0}", partOne);
            Console.WriteLine("Part two = {0}", partTwo);
            Console.ReadLine(); 

        }

        private static int GetTotalDistance(IReadOnlyDictionary<string, int> distances, IReadOnlyList<char> locations)
        {
            int totalDistance = 0;

            for (int i = 0; i < locations.Count-1; i++)
            {
                string key = locations[i] + "" + locations[i + 1];
                totalDistance += distances[key];
            }

            return totalDistance;
        }


        private static char[,] CreateMaze(IReadOnlyList<string> instructions)
        {
            char[,] maze = new char[instructions[0].Length, instructions.Count];

            for (int y = 0; y < instructions.Count; y++)
            {
                for (int x = 0; x < instructions[y].Length; x++)
                {
                    maze[x, y] = instructions[y][x];
                }
            }

            return maze;
        }

        private static MazeState FindShortestPath(char[,] maze, Point start, Point target)
        {
            Queue<MazeState> statesQueue = new Queue<MazeState>(new[] { new MazeState(start, 0) });
            MazeState finishState = null;

            while (statesQueue.Count > 0)
            {
                MazeState currentState = statesQueue.Dequeue();

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
                        if (IsOpenSpace(maze, newState.Position.X, newState.Position.Y) && !statesQueue.Contains(newState))
                        {
                            statesQueue.Enqueue(newState);
                        }
                    }
                }
            }

            return finishState;
        }

        private static bool IsOpenSpace(char[,] maze, int x, int y)
        {
            return x >= 0 && y >= 0 && maze[x, y] != '#';
        }

        public static Point FindPoint(char[,] maze, char pointSymbol)
        {
            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    if (maze[x,y] == pointSymbol)
                    {
                        return new Point(x, y);
                    }
                }
            }

            return new Point();
        }
    }

    public class PathDistance : IComparable
    {
        public List<char> Locations { get; set; }
        public int Distance { get; set; }
        public int CompareTo(object obj)
        {
            if(obj == null)
            {
                return 1;
            }

            PathDistance otherPathDistance = obj as PathDistance;
            if (otherPathDistance != null)
            {
                return Distance.CompareTo(otherPathDistance.Distance);
            }
            throw new ArgumentException("Object is not a PathDistance");
        }
    }

}
