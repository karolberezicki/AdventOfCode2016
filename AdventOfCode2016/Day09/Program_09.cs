using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09
{
    public class Program_09
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            StringBuilder sb = Decompress(source);

            int partOneDecompressedLength = sb.Length;
            long partTwoDecompressedLength = DecompressLengthV2(source);

            Console.WriteLine("Part one = {0}", partOneDecompressedLength);
            Console.WriteLine("Part two = {0}", partTwoDecompressedLength);
            Console.ReadLine();

        }

        public static StringBuilder Decompress(string source)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == '(')
                {

                    StringBuilder marker = new StringBuilder();
                    marker.Append(source[i]);
                    for (int j = i + 1; j <= source.Length; j++)
                    {
                        marker.Append(source[j]);
                        if (source[j] == ')')
                        {
                            break;
                        }
                    }
                    string[] markerValues = marker.ToString().Split('(', ')')[1].Split('x');

                    int subsequentCharacters = int.Parse(markerValues[0]);
                    int repeat = int.Parse(markerValues[1]);

                    StringBuilder repeatedChars = new StringBuilder();
                    for (int j = i + marker.Length; j < i + marker.Length + subsequentCharacters; j++)
                    {
                        repeatedChars.Append(source[j]);
                    }


                    for (int j = 0; j < repeat; j++)
                    {
                        sb.Append(repeatedChars);
                    }

                    i += subsequentCharacters + marker.Length - 1;
                    continue;

                }
                else
                {
                    sb.Append(source[i]);
                }
            }

            return sb;
        }

        public static long DecompressLengthV2(string source)
        {
            if (!source.Contains('('))
            {
                return source.Length;
            }
                
            long decompressedLength = 0;

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] != '(')
                {
                    decompressedLength++;
                    continue;
                }

                StringBuilder marker = new StringBuilder();
                marker.Append(source[i]);
                for (int j = i + 1; j <= source.Length; j++)
                {
                    marker.Append(source[j]);
                    if (source[j] == ')')
                    {
                        break;
                    }
                }
                string[] markerValues = marker.ToString().Split('(', ')')[1].Split('x');

                int subsequentCharacters = int.Parse(markerValues[0]);
                int repeatDecompressed = int.Parse(markerValues[1]);

                int repeat = repeatDecompressed.ToString().Length + subsequentCharacters.ToString().Length + 3;
                string partToDecompress = source.Substring(i + repeat, subsequentCharacters);

                decompressedLength += DecompressLengthV2(partToDecompress) * repeatDecompressed;
                i += repeat + subsequentCharacters - 1;
            }
            
            return decompressedLength;
        }
    }
}
