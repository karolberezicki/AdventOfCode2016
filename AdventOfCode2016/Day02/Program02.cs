using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day02
{
    public class Program02
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').ToList();

            // Keypads are surrounded with zeros to skip handling out of bounds exception

            char[] keypad1 = {
                 '0', '0', '0', '0', '0',
                 '0', '1', '2', '3', '0',
                 '0', '4', '5', '6', '0',
                 '0', '7', '8', '9', '0',
                 '0', '0', '0', '0', '0'
            };

            string passcode1 = GetPasscode(keypad1, 12, instructions);

            char[] keypad2 = {
               '0', '0', '0', '0', '0', '0', '0',
               '0', '0', '0', '1', '0', '0', '0',
               '0', '0', '2', '3', '4', '0', '0',
               '0', '5', '6', '7', '8', '9', '0',
               '0', '0', 'A', 'B', 'C', '0', '0',
               '0', '0', '0', 'D', '0', '0', '0',
               '0', '0', '0', '0', '0', '0', '0'
            };

            string passcode2 = GetPasscode(keypad2, 22, instructions);

            Console.WriteLine("Part one = {0}", passcode1);
            Console.WriteLine("Part two = {0}", passcode2);
            Console.ReadLine();
        }


        public static string GetPasscode(char[] keypad, int startLocation, List<string> instructions)
        {
            string passcode = "";

            int verticalChange = (int)Math.Sqrt(keypad.Length);

            int currentLocation = startLocation;
            foreach (string instruction in instructions)
            {

                for (int i = 0; i < instruction.Length; i++)
                {
                    int change = 0;

                    switch (instruction[i])
                    {
                        case 'U':
                            change = -verticalChange;
                            break;
                        case 'D':
                            change = verticalChange;
                            break;
                        case 'L':
                            change = -1;
                            break;
                        case 'R':
                            change = 1;
                            break;
                    }
                    // Skip change, if it's outside of actual keypad
                    if (keypad[currentLocation + change] == '0')
                    {
                        continue;
                    }
                    currentLocation = currentLocation + change;

                }
                passcode = passcode + keypad[currentLocation];
            }

            return passcode;
        }
    }
}
