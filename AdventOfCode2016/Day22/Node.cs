using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Day22
{
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
}