using System;
using System.Linq;
using System.Text;

namespace Day16
{
    public class Program_16
    {
        public static void Main(string[] args)
        {
            const string input = "01110110101001000";
            const int partOneDiskSpaceToFill = 272;
            const int partTwoDiskSpaceToFill = 35651584;

            string partOne = GetCheckSumForFilledDiskSpace(input, partOneDiskSpaceToFill);
            string partTwo = GetCheckSumForFilledDiskSpace(input, partTwoDiskSpaceToFill);

            Console.WriteLine("Part one = {0}", partOne);
            Console.WriteLine("Part two = {0}", partTwo);
            Console.ReadLine();

        }

        private static string GetCheckSumForFilledDiskSpace(string input, int diskSpaceToFill)
        {
            string data = input;

            while (data.Length < diskSpaceToFill)
            {
                data = DragonCurve(data);
            }

            data = data.Truncate(diskSpaceToFill);

            return CalculateChecksum(data);
        }

        public static string DragonCurve(string a)
        {
            string b = a;
            b = new string(b.Reverse().ToArray());
            b = b.Replace("0", "#").Replace("1", "0").Replace("#", "1");
            return string.Join("0", a, b);
        }


        public static string CalculateChecksum(string data)
        {
            StringBuilder checksumBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i+=2)
            {
                checksumBuilder.Append(data[i] == data[i + 1] ? "1" : "0");
            }

            string checksum = checksumBuilder.ToString();

            return checksum.Length % 2 == 0 ? CalculateChecksum(checksum) : checksum;
        }

    }


    public static class StringExtensions
    {
        public static string Truncate(this string str, int maxLength)
        {
            return str.Substring(0, Math.Min(str.Length, maxLength));
        }
    }
}
