using System.Diagnostics;

namespace Day10
{
    public partial class Program10
    {

        [DebuggerDisplay("Number = {Number}, Value = {Value}")]
        public class Output
        {
            public Output(int number, int value)
            {
                Number = number;
                Value = value;
            }

            public int Number { get; set; }
            public int Value { get; set; }
        }
    }
}
