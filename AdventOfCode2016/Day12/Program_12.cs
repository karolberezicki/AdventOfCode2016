using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    public class Program_12
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').ToList();

            Dictionary<char, int> registers = new Dictionary<char, int>
            {
                {'a', 0},
                {'b', 0},
                {'c', 0},
                {'d', 0}
            };

            RunAssembunny(instructions, registers);

            Console.WriteLine("Part one = {0}",registers['a']);

            registers = new Dictionary<char, int>
            {
                {'a', 0},
                {'b', 0},
                {'c', 1},
                {'d', 0}
            };

            RunAssembunny(instructions, registers);

            Console.WriteLine("Part two = {0}", registers['a']);

            Console.ReadLine();
        }

        private static void RunAssembunny(IReadOnlyList<string> instructions, IDictionary<char, int> registers)
        {
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
                        registers[parts[2].First()] = copyValue;
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
                        break;
                }
            }
        }
    }
}
