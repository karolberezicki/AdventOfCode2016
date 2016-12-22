using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day22
{
    public class Program22
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').Skip(2).ToList();

            List<Node> nodes = instructions.Select(i => new Node(i)).ToList();

            int viablePairsCount = GetViablePairsCount(nodes);

        }

        private static int GetViablePairsCount(List<Node> nodes)
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

        private static List<Node> GetAdjacentNodes(List<Node> nodes, Node node)
        {
            int nodeX = node.Point.X;
            int nodeY = node.Point.Y;
            List<Node> adjacentNodes = new List<Node>();
            adjacentNodes.AddRange(nodes.Where(n => n.Point.X == nodeX - 1 && n.Point.Y == nodeY));
            adjacentNodes.AddRange(nodes.Where(n => n.Point.X == nodeX + 1 && n.Point.Y == nodeY));
            adjacentNodes.AddRange(nodes.Where(n => n.Point.X == nodeX && n.Point.Y == nodeY - 1));
            adjacentNodes.AddRange(nodes.Where(n => n.Point.X == nodeX && n.Point.Y == nodeY + 1));
            return adjacentNodes;
        }
    }

    [DebuggerDisplay("X = {Point.X}, Y = {Point.Y},  Size = {Size}, Used = {Used}, Avail = {Avail}, UsePercent = {UsePercent}")]
    public class Node
    {
        public Point Point { get; set; }
        public int Size { get; set; }
        public int Used { get; set; }
        public int Avail { get; set; }
        public int UsePercent { get; set; }

        public Node(string node)
        {
            node =  node.Replace("T", "")
                .Replace("%", "")
                .Replace("/dev/grid/node", "")
                .Replace("-x", "")
                .Replace("-y", " ");

            node = Regex.Replace(node, @"\s+", " ");

            string[] parts = node.Split(' ');

            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            Point = new Point(x, y);
            Size = int.Parse(parts[2]);
            Used = int.Parse(parts[3]);
            Avail = int.Parse(parts[4]);
            UsePercent = int.Parse(parts[5]);
        }
    }

    public class Pair
    {
        public Pair(Point a, Point b)
        {
            PointA = a;
            PointB = b;
        }

        public Point PointA { get; }
        public Point PointB { get; }

        public override bool Equals(object obj)
        {
            Pair item = obj as Pair;
            return item != null 
                && (PointA.Equals(item.PointA) && PointB.Equals(item.PointB)
                || PointB.Equals(item.PointA) && PointA.Equals(item.PointB));
        }

        public override int GetHashCode()
        {
            return PointA.GetHashCode() ^ PointB.GetHashCode();
        }
    }

}
