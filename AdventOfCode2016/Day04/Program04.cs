using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day04
{
    public class Program04
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').ToList();

            int sumOfRealSectorIds = 0;
            List<KeyValuePair<string, int>> decryptedRooms = new List<KeyValuePair<string, int>>();

            foreach (string instruction in instructions)
            {
                Dictionary<char, int> lettersCount = GetEmptyLettersDictionary();

                string[] parts = instruction.Split('[');
                string name = parts[0];
                string nameWithoutNumber = string.Concat(name.Where(n => !char.IsNumber(n)));
                int id = int.Parse(parts[0].Split('-').Last());
                string checksum = parts[1].Replace("]", "").Replace("\r", "");
                int checksumLength = checksum.Length;

                foreach (char letter in name)
                {
                    if (char.IsLetter(letter))
                    {
                        lettersCount[letter] = lettersCount[letter] + 1;
                    }
                }

                List<KeyValuePair<char, int>> lettersList = lettersCount.ToList();
                string checkStr = string.Concat(
                    lettersList.OrderByDescending(a => a.Value)
                    .ThenBy(a => a.Key)
                    .Take(checksumLength)
                    .Select(a => a.Key));

                if (checksum == checkStr)
                {
                    sumOfRealSectorIds += id;

                    StringBuilder decryptedName = new StringBuilder();

                    for (int i = 0; i < nameWithoutNumber.Length; i++)
                    {
                        if (nameWithoutNumber[i] == '-')
                        {
                            decryptedName.Append(" ");
                        }
                        else
                        {
                            int index = lettersList.FindIndex(a => a.Key == nameWithoutNumber[i]);
                            int decryptedIndex = (index + id) % lettersList.Count;
                            decryptedName.Append(lettersList[decryptedIndex].Key);
                        }
                    }

                    decryptedRooms.Add(new KeyValuePair<string, int>(decryptedName.ToString(), id));
                }
            }

            int northPoleStorage = decryptedRooms.Where(room => room.Key.Contains("northpole")).First().Value;

            Console.WriteLine("Part one = {0}", sumOfRealSectorIds);
            Console.WriteLine("Part two = {0}", northPoleStorage);
            Console.ReadLine();
        }

        private static Dictionary<char, int> GetEmptyLettersDictionary()
        {
            Dictionary<char, int> lettersCount = new Dictionary<char, int>();
            for (char letter = 'a'; letter <= 'z'; letter++)
            {
                lettersCount[letter] = 0;
            }

            return lettersCount;
        }
    }
}
