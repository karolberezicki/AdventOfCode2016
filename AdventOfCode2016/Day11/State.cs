using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day11
{
    [Serializable]
    public class State
    {
        public int Elevator { get; set; }
        public List<HashSet<string>> Floors { get; set; }
        public int Move { get; set; }

        public bool IsValid => Floors.All(IsVaildFloor);

        private static bool IsVaildFloor(HashSet<string> floor)
        {
            bool containsPair = floor.Where(e => e.EndsWith("M"))
                .Any(element => floor.Contains(element.Replace("M", "G")));

            bool hasSingleChip = floor.Where(e => e.EndsWith("M"))
                .Any(element => !floor.Contains(element.Replace("M", "G")));

            return !containsPair || !hasSingleChip;
        }

        public override bool Equals(object obj)
        {
            State stateObj = obj as State;

            if (Elevator != stateObj?.Elevator)
            {
                return false;
            }

            if (Floors.Count != stateObj.Floors.Count)
            {
                return false;
            }

            return !Floors.Where((t, i) => !t.SetEquals(stateObj.Floors[i])).Any();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Floors.Aggregate(19, (current, floor) => current * 31 + floor.GetHashCode());
                return hash ^ Elevator.GetHashCode();
            }
        }

        public string GetCode()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Elevator}E");

            foreach (HashSet<string> floor in Floors)
            {
                int countMicrochips = floor.Count(e => e.EndsWith("M"));
                int countGenerators = floor.Count(e => e.EndsWith("G"));
                sb.Append($"{countMicrochips}M{countGenerators}G");
            }
            return sb.ToString();
        }

    }
}