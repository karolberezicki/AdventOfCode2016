using System.Collections.Generic;

namespace Day03
{
    public class Triangle
    {
        public Triangle(List<string> points) : this(points[0], points[1], points[2])
        {
                        
        }

        public Triangle(string a, string b, string c)
        {
            A = int.Parse(a);
            B = int.Parse(b);
            C = int.Parse(c);
        }

        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }

        public bool IsValidTriangle
        {
            get { return (A + B > C && A + C > B && B + C > A); }
        }
    }
}
