using System.Diagnostics;

namespace Day15
{
    [DebuggerDisplay("Number = {Number}, OrderNumber = {OrderNumber},  PossiblePositions = {PossiblePositions}, CurrentPosition = {CurrentPosition}, IsAligned = {IsAligned}")]
    public class Disk
    {
        public int Number { get; set; }
        public int OrderNumber { get; set; }
        public int PossiblePositions { get; set; }
        public int CurrentPosition { get; set; }


        public void IncrementPosition()
        {
            CurrentPosition++;

            if (CurrentPosition > PossiblePositions - 1)
            {
                CurrentPosition = 0;
            }
        }

        public bool IsAligned => PossiblePositions < OrderNumber
            ? CurrentPosition == OrderNumber - PossiblePositions
            : CurrentPosition == OrderNumber;
    }
}