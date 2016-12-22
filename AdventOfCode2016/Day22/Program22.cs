using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day22
{
    public class Program22
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').Skip(2).ToList();

            HashSet<Node> nodes = instructions.Select(i => new Node(i)).ToHashSet();

            int partOne = GetViablePairsCount(nodes);

            Node goal = nodes.Where(n => n.Point.Y == 0).Aggregate((n1, n2) => n1.Point.X > n2.Point.X ? n1 : n2);

            Node emptyNode = nodes.Aggregate((n1, n2) => n1.Avail > n2.Avail ? n1 : n2);

            List<Node> wallNodes = nodes.Where(n => n.Used > emptyNode.Avail).ToList();
            Node wallNodeWithMinX = wallNodes.Aggregate((n1, n2) => n1.Point.X < n2.Point.X ? n1 : n2);

            int partTwo = (goal.Point.X - 1) * 5 +
                          emptyNode.Point.Y +
                          wallNodes.Count + (emptyNode.Point.X - wallNodeWithMinX.Point.X) + 1;

            Console.WriteLine("Part one = {0}", partOne);
            Console.WriteLine("Part two = {0}", partTwo);
            Console.ReadLine();

        }

        private static int GetViablePairsCount(HashSet<Node> nodes)
        {
            HashSet<Pair> viablePairs = new HashSet<Pair>();

            foreach (Node node in nodes)
            {
                foreach (Node otherNode in nodes)
                {
                    Pair pair = new Pair(node.Point, otherNode.Point);

                    if (node.Used != 0 && otherNode.Avail >= node.Used && !viablePairs.Contains(pair))
                    {
                        viablePairs.Add(pair);
                    }
                }

            }

            return viablePairs.Count;
        }
    }
}
