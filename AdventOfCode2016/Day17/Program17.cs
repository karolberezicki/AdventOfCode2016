using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day17
{
    public class Program17
    {
        public static void Main(string[] args)
        {
            const string input = "njfxhljp";

            Queue<State> statesQueue = new Queue<State>(new[] { new State() });
            string shortestPath = null;
            string longestPaht = "";

            Point target = new Point(3, 3);

            while (statesQueue.Count > 0)
            {
                State currentState = statesQueue.Dequeue();
                if (currentState.Position == target)
                {
                    if (shortestPath == null || shortestPath.Length > currentState.Path.Length)
                    {
                        shortestPath = currentState.Path;
                    }

                    if (currentState.Path.Length > longestPaht.Length)
                    {
                        longestPaht = currentState.Path;
                    }
                }
                else
                {
                    string hash = Utils.CreateMd5(input + currentState.Path);
                    if (IsUlocked(hash[0]) && currentState.Position.Y > 0)
                    {
                        statesQueue.Enqueue(new State(currentState, "U"));
                    }

                    if (IsUlocked(hash[1]) && currentState.Position.Y < target.Y)
                    {
                        statesQueue.Enqueue(new State(currentState, "D"));
                    }

                    if (IsUlocked(hash[2]) && currentState.Position.X > 0)
                    {
                        statesQueue.Enqueue(new State(currentState, "L"));
                    }

                    if (IsUlocked(hash[3]) && currentState.Position.X < target.X)
                    {
                        statesQueue.Enqueue(new State(currentState, "R"));
                    }
                }
            } 

            Console.WriteLine("Part one = {0}", shortestPath);
            Console.WriteLine("Part two = {0}", longestPaht.Length);
            Console.ReadLine();

        }


        public static bool IsUlocked(char doorCode)
        {
            char[] unlock = {'b','c','d','e','f'};
            return unlock.Contains(doorCode);
        }

    }
}
