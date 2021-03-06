﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day25
{
    public class Program25
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').ToList();

            const string clockSignal = "01010101";

            Dictionary<char, int> registers = new Dictionary<char, int>
            {
                {'a', 0},
                {'b', 0},
                {'c', 0},
                {'d', 0}
            };

            int partOne = 0;
            while (!RunAssembunny(instructions, registers, clockSignal))
            {
                partOne++;
                registers['a'] = partOne;
            }

            Console.WriteLine("Part one = {0}", partOne);
            //Console.WriteLine("Part two = {0}", partTwo);
            Console.ReadLine();
        }

        private static bool RunAssembunny(IList<string> instructions, IDictionary<char, int> registers, string clockSignal)
        {
            string output = "";

            for (int i = 0; i < instructions.Count; i++)
            {
                string[] parts = instructions[i].Split(' ');

                char register;

                switch (parts[0])
                {
                    case "inc":
                        register = parts[1].First();
                        registers[register] = registers[register] + 1;
                        break;
                    case "dec":
                        register = parts[1].First();
                        registers[register] = registers[register] - 1;
                        break;
                    case "cpy":
                        int copyValue;
                        if (!int.TryParse(parts[1], out copyValue))
                        {
                            register = parts[1].First();
                            copyValue = registers[register];
                        }

                        int invalidRegister;
                        if (!int.TryParse(parts[2], out invalidRegister))
                        {
                            registers[parts[2].First()] = copyValue;
                        }

                        break;
                    case "jnz":
                        int firstOperand;
                        int jumpValue;

                        if (!int.TryParse(parts[1], out firstOperand))
                        {
                            firstOperand = registers[parts[1].First()];
                        }

                        if (int.TryParse(parts[2], out jumpValue) && firstOperand != 0 && jumpValue != 0)
                        {

                            jumpValue -= 1;
                            i += jumpValue;
                        }
                        else if (registers.ContainsKey(parts[2].First()) && firstOperand != 0)
                        {
                            jumpValue = registers[parts[2].First()] - 1;
                            i += jumpValue;
                        }

                        break;

                    case "tgl":
                        int toggleValue;
                        if (!int.TryParse(parts[1], out toggleValue))
                        {
                            register = parts[1].First();
                            toggleValue = registers[register];
                        }


                        if (i + toggleValue >= instructions.Count)
                        {
                            break;
                        }

                        string toggledInstruction = instructions[i + toggleValue];

                        if (toggledInstruction.Contains("inc"))
                        {
                            instructions[i + toggleValue] = toggledInstruction.Replace("inc", "dec");
                        }
                        else if (toggledInstruction.Contains("dec") || toggledInstruction.Contains("tgl"))
                        {
                            instructions[i + toggleValue] = toggledInstruction.Replace("dec", "inc").Replace("tgl", "inc");

                        }
                        else if (toggledInstruction.Contains("jnz"))
                        {
                            instructions[i + toggleValue] = toggledInstruction.Replace("jnz", "cpy");
                        }
                        else if (toggledInstruction.Contains("cpy"))
                        {
                            instructions[i + toggleValue] = toggledInstruction.Replace("cpy", "jnz");
                        }
                        break;
                    case "out":
                        int outValue;
                        if (!int.TryParse(parts[1], out outValue))
                        {
                            register = parts[1].First();
                            outValue = registers[register];
                        }

                        if (output.Length != clockSignal.Length)
                        {
                            output = output + outValue;
                        }
                        else
                        {
                            return output == clockSignal;
                        }

                        break;
                }
            }

            return false;
        }

    }
}
