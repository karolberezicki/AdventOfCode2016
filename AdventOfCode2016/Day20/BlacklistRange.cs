namespace Day20
{
    public class BlacklistRange
    {
        public long LowerLimit { get; set; }
        public long UpperLimit { get; set; }

        public BlacklistRange(string input)
        {
            string[] parts = input.Split('-');
            LowerLimit = long.Parse(parts[0]);
            UpperLimit = long.Parse(parts[1]);
        }

    }
}