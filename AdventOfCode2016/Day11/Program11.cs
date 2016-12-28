using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
{
    public class Program11
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> instructions = source.Split('\n').ToList();
            List<HashSet<string>> floors = GenerateInitalFloors(instructions);
            DisplayFloors(floors);

            State startState = new State { Elevator = 0, Floors = floors, Move = 0 };
            HashSet<string> finishFloor = GetFinishFloor(floors);
            State finishState = Solve(startState, finishFloor);

            int partOne = finishState.Move;

            Console.WriteLine("Part one = {0}", partOne);


            instructions[0] = instructions[0] +
                " an elerium generator, an elerium-compatible microchip, a dilithium generator, a dilithium-compatible microchip";
            floors = GenerateInitalFloors(instructions);
            DisplayFloors(floors);

            startState = new State { Elevator = 0, Floors = floors, Move = 0 };
            finishFloor = GetFinishFloor(floors);
            State finishStatePartTwo = Solve(startState, finishFloor);

            int partTwo = finishStatePartTwo.Move;

            Console.WriteLine("Part two = {0}", partTwo);

            Console.ReadLine();
        }

        private static State Solve(State startState, HashSet<string> finishFloor)
        {
            Queue<State> queue = new Queue<State>();
            HashSet<string> seenStates = new HashSet<string>();

            queue.Enqueue(startState);
            seenStates.Add(startState.GetCode());
            while (queue.Count > 0)
            {
                State currentState = queue.Dequeue();

                if (currentState.Floors.Last().SetEquals(finishFloor))
                {
                    return currentState;
                }

                List<State> newStates = new List<State>();

                List<string> currentFloor = currentState.Floors[currentState.Elevator].ToList();

                if (currentState.Elevator != 0)
                {
                    // Add states downwards
                    foreach (string element in currentFloor)
                    {
                        State newState = Utils.DeepClone(currentState);
                        newState.Elevator = newState.Elevator - 1;
                        newState.Move = newState.Move + 1;


                        newState.Floors[currentState.Elevator].Remove(element);
                        newState.Floors[currentState.Elevator - 1].Add(element);
                        newStates.Add(newState);
                    }
                }

                if (currentState.Elevator != currentState.Floors.Count - 1)
                {
                    // Add states upwards
                    foreach (string firstElement in currentFloor)
                    {
                        List<string> currentFloorExceptFirstElement = currentFloor.Where(e => e != firstElement).ToList();

                        foreach (string secondElement in currentFloorExceptFirstElement)
                        {
                            State newState = Utils.DeepClone(currentState);
                            newState.Elevator = newState.Elevator + 1;
                            newState.Move = newState.Move + 1;

                            newState.Floors[currentState.Elevator].Remove(firstElement);
                            newState.Floors[currentState.Elevator + 1].Add(firstElement);
                            newState.Floors[currentState.Elevator].Remove(secondElement);
                            newState.Floors[currentState.Elevator + 1].Add(secondElement);

                            newStates.Add(newState);

                        }
                    }
                }


                newStates = newStates.Where(s => s.IsValid && !queue.Contains(s) && !seenStates.Contains(s.GetCode())).ToList();
                foreach (State newState in newStates)
                {
                    seenStates.Add(newState.GetCode());
                    queue.Enqueue(newState);
                }
            }

            return null;
        }

        private static HashSet<string> GetFinishFloor(IEnumerable<HashSet<string>> floors)
        {
            HashSet<string> finishFloor = new HashSet<string>();
            foreach (HashSet<string> floor in floors)
            {
                finishFloor.UnionWith(floor);
            }

            return finishFloor;
        }

        private static List<HashSet<string>> GenerateInitalFloors(IEnumerable<string> instructions)
        {
            List<HashSet<string>> floors = new List<HashSet<string>>();

            foreach (string instruction in instructions)
            {
                HashSet<string> floor = new HashSet<string>();

                string[] parts = instruction.Replace(",", "").Replace(".", "").Replace("\r", "").Split(' ');

                for (int i = 0; i < parts.Length; i++)
                {
                    if (parts[i] != "generator" && parts[i] != "microchip")
                    {
                        continue;
                    }

                    string machinePart = ("" + parts[i - 1][0] + parts[i - 1][1] + parts[i][0]).ToUpper();
                    floor.Add(machinePart);
                }

                floors.Add(floor);

            }

            return floors;
        }

        private static void DisplayFloors(IList<HashSet<string>> floors)
        {
            foreach (HashSet<string> list in floors)
            {
                Console.Write("Floor {0}: ", floors.IndexOf(list) + 1);
                Console.WriteLine(string.Join(", ", list));
            }
        }
    }
}
