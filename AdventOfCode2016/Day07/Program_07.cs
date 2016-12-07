using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day07
{
    public class Program_07
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            List<string> instructions = source.Split('\n').ToList();

            int countSupportingTLS = 0;
            foreach (string instruction in instructions)
            {

                string[] ipParts = instruction.Split('[', ']');


                List<bool> tls = new List<bool>();

                for (int i = 0; i < ipParts.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        tls.Add(HasABBA(ipParts[i]));
                    }
                    else
                    {
                        tls.Add(!HasABBA(ipParts[i]));
                    }
                }


                IEnumerable<bool> evenTls = tls.ToList().Where((c, i) => i % 2 == 0);
                IEnumerable<bool> oddTls = tls.ToList().Where((c, i) => i % 2 != 0);

                if (evenTls.Any(a => a) && oddTls.All(a => a))
                {
                    countSupportingTLS++;
                }
            }

            int countSupportingSSL = instructions
               .Select(i => i.Split('[', ']'))
               .Select(i => new List<IEnumerable<string>>
               {
                   i.Where((c, a) => a % 2 == 0)
                   .SelectMany(a => GetABA(a))
                   .Select(aba => ConvertABAToBAB(aba)),
                   i.Where((c, a) => a % 2 != 0)
               }).Count(i => ContainsBAB(i[0], i[1]));


            Console.WriteLine("Part one = {0}", countSupportingTLS);
            Console.WriteLine("Part two = {0}", countSupportingSSL);
            Console.ReadLine();
        }        

        public static string ConvertABAToBAB(string aba)
        {
            return string.Join("", aba[1], aba[0], aba[1]);
        }

        public static bool ContainsBAB(IEnumerable<string> abaList, IEnumerable<string> hypernetSequences)
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

        public static List<string> GetABA(string supernetSequence)
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

        public static bool HasABBA(string ipPart)
        {
            for (int i = 0; i < ipPart.Length - 3; i++)
            {
                if (ipPart[i] == ipPart[i + 3] && ipPart[i + 1] == ipPart[i + 2] && ipPart[i] != ipPart[i + 1])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
