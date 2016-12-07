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
            source = source.Remove(source.Length - 1);
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

            int countSupportingSSL = 0;

            foreach (string instruction in instructions)
            {
                string[] ipParts = instruction.Split('[', ']');

                List<string> abaList = new List<string>();


                for (int i = 0; i < ipParts.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        abaList.AddRange(GetABA(ipParts[i]));
                    }
                }


                List<string> babList = abaList.Select(aba => string.Join("", aba[1], aba[0], aba[1])).ToList();


                bool containsBab = false;


                for (int i = 0; i < ipParts.Length; i++)
                {
                    if (i % 2 == 1)
                    {
                        foreach (string bab in babList)
                        {

                            if (ipParts[i].Contains(bab))
                            {
                                containsBab = true;
                            }

                        }
                    }
                }

                if (containsBab)
                {
                    countSupportingSSL++;
                }
            }

            Console.WriteLine("Part one = {0}", countSupportingTLS);
            Console.WriteLine("Part two = {0}", countSupportingSSL);
            Console.ReadLine();
        }


        public static List<string> GetABA(string ipPart)
        {
            List<string> abaList = new List<string>();
            for (int i = 0; i < ipPart.Length - 2; i++)
            {
                if (ipPart[i] == ipPart[i + 2] && ipPart[i] != ipPart[i + 1])
                {
                    abaList.Add(string.Join("", ipPart[i], ipPart[i + 1], ipPart[i + 2]));
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
