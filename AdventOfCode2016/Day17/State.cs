using System.Drawing;

namespace Day17
{
    public class State
    {
        public readonly string Path = "";
        public Point Position = new Point(0, 0);

        public State()
        {

        }

        public State(State previousState, string direction)
        {
            Path = previousState.Path + direction;
            Position = previousState.Position;

            switch (direction)
            {
                case "U":
                    Position.Y = Position.Y - 1;
                    break;
                case "D":
                    Position.Y = Position.Y + 1;
                    break;
                case "L":
                    Position.X = Position.X - 1;
                    break;
                case "R":
                    Position.X = Position.X + 1;
                    break;
            }
        }

    }
}