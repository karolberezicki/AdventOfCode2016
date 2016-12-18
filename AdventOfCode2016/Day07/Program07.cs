using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day07
{
    public class Program07
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            List<string> instructions = source.Split('\n').ToList();

            int countSupportingTls = instructions
               .Select(i => i.Split('[', ']'))
               .Select(i => new List<IEnumerable<bool>>
               {
                   i.Where((c, a) => a % 2 == 0).Select(HasAbba),
                   i.Where((c, a) => a % 2 != 0).Select(HasAbba)
               }).Count(i => i[0].Any(a => a) && i[1].All(a => !a));

            int countSupportingSsl = instructions
               .Select(i => i.Split('[', ']'))
               .Select(i => new List<IEnumerable<string>>
               {
                   i.Where((c, a) => a % 2 == 0)
                   .SelectMany(GetAba)
                   .Select(ConvertAbatoBab),
                   i.Where((c, a) => a % 2 != 0)
               }).Count(i => ContainsBab(i[0], i[1]));


            Console.WriteLine("Part one = {0}", countSupportingTls);
            Console.WriteLine("Part two = {0}", countSupportingSsl);
            Console.ReadLine();
        }        

        public static string ConvertAbatoBab(string aba)
        {
            return string.Join("", aba[1], aba[0], aba[1]);
        }

        public static bool ContainsBab(IEnumerable<string> abaList, IEnumerable<string> hypernetSequences)
        {
            foreach (string hypernetSequence in hypernetSequences)
            {
                if (abaList.Any(hypernetSequence.Contains))
                {
                    return true;
                }
            }
            return false;
        }

        public static List<string> GetAba(string supernetSequence)
        {
            List<string> abaList = new List<string>();
            for (int i = 0; i < supernetSequence.Length - 2; i++)
            {
                if (supernetSequence[i] == supernetSequence[i + 2] && supernetSequence[i] != supernetSequence[i + 1])
                {
                    abaList.Add(string.Join("", supernetSequence[i], supernetSequence[i + 1], supernetSequence[i + 2]));
                }
            }
            return abaList;
        }

        public static bool HasAbba(string sequence)
        {
            for (int i = 0; i < sequence.Length - 3; i++)
            {
                if (sequence[i] == sequence[i + 3] && sequence[i + 1] == sequence[i + 2] && sequence[i] != sequence[i + 1])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
