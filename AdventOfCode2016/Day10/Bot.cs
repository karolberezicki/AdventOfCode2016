using System.Collections.Generic;

namespace Day10
{
    public partial class Program10
    {
        public class Bot
        {
            public Bot(string definition)
            {
                string[] parts = definition.Split(' ');

                Number = int.Parse(parts[1]);
                GiveLowValueTo = int.Parse(parts[6]);
                GiveHighValueTo = int.Parse(parts[11]);
                Values = new List<int>();

                if (parts[5] == "output")
                {
                    GivesLowToOutput = true;
                }

                if (parts[10] == "output")
                {
                    GivesHighToOutput = true;
                }

            }

            public int Number { get; set; }
            public List<int> Values { get; set; }
            public int GiveLowValueTo { get; set; }
            public int GiveHighValueTo { get; set; }
            public bool GivesLowToOutput { get; set; }
            public bool GivesHighToOutput { get; set; }

        }
    }
}
