﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    public class Program_14
    {
        public static void Main(string[] args)
        {
            const string input = "yjdafjpo";

            int partOne = GetIndexOf64ThKey(input);
            int partTwo = GetIndexOf64ThKey(input, 2016);

            Console.WriteLine("Part one = {0}", partOne);
            Console.WriteLine("Part two = {0}", partTwo);
            Console.ReadLine();


        }

        private static int GetIndexOf64ThKey(string input, int stretch = 0)
        {
            char[] alpha = "abcdefghijklmnopqrstuvwxyz1234567890".ToCharArray();

            List<string> keys = new List<string>();
            List<Triplet> triplets = new List<Triplet>();

            int index = 0;
            while (keys.Count < 64)
            {
                string toHash = input + index;
                string hash = Utils.CreateMd5MultiHash(toHash, stretch + 1);

                foreach (char character in alpha)
                {
                    if (hash.Contains(new string(character, 3)))
                    {
                        triplets.Add(new Triplet
                        {
                            Hash = hash,
                            RepeatedChar = character,
                            Index = index
                        });

                        break;
                    }
                }

                List<Triplet> tripletsToSearch = triplets.Where(t => t.Index < index && t.Index >= index - 1000).ToList();

                foreach (Triplet triplet in tripletsToSearch)
                {
                    if (hash.Contains(new string(triplet.RepeatedChar, 5)))
                    {
                        keys.Add(triplet.Hash);
                        if (keys.Count > 64)
                        {
                            break;
                        }
                    }
                }

                index++;

            }

            int tripletIndexOfLastKey = triplets.First(t => t.Hash == keys.Last()).Index;
            return tripletIndexOfLastKey;
        }
    }
}
