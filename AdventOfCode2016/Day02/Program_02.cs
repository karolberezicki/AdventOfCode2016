using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    public class Program_02
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            //source = source.Replace(" ", "");
            List<string> intructions = source.Split('\n').ToList();


            string passcode = "";

            foreach (string intruction in intructions)
            {

                int number = 5;

                for (int i = 0; i < intruction.Length; i++)
                {
                    int change = 0;

                    switch (intruction[i])
                    {
                        case 'U':
                            change = -3;
                            break;
                        case 'D':
                            change = 3;
                            break;
                        case 'L':
                            change = -1;
                            break;
                        case 'R':
                            change = 1;
                            break;
                        default:
                            break;
                    }

                    if (number < 4 && change == -3)
                    {
                        continue;
                    }

                    if (number > 6 && change == 3)
                    {
                        continue;
                    }

                    if (number % 3 == 0 && change == 1)
                    {
                        continue;
                    }

                    if ((number +2) % 3 == 0 && change == -1)
                    {
                        continue;
                    }

                    number = number + change;
                }

                passcode = passcode + number;                


            }



            string passcode2 = "";


            char[] keypad = {

                '0', '0', '1', '0', '0',
                '0', '2', '3', '4', '0',
                '5', '6', '7', '8', '9',
                '0', 'A', 'B', 'C', '0',
                '0', '0', 'D', '0', '0'
            };

            int number2 = 10;
            foreach (string intruction in intructions)
            {
                
                for (int i = 0; i < intruction.Length; i++)
                {
                    int change = 0;

                    switch (intruction[i])
                    {
                        case 'U':
                            change = -5;
                            break;
                        case 'D':
                            change = 5;
                            break;
                        case 'L':
                            change = -1;
                            break;
                        case 'R':
                            change = 1;
                            break;
                        default:
                            break;
                    }

                    if (number2 < 4 && change == -5)
                    {
                        continue;
                    }

                    if (number2 > 19 && change == 5)
                    {
                        continue;
                    }

                    if (keypad[number2 + change] == '0')
                    {
                        continue;
                    }

                    number2 = number2 + change;


                }
                passcode2 = passcode2 + keypad[number2];

            }


        }
    }
}
