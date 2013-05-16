using System;

namespace MartianRobots
{
    public class Position
    {
        public Position(int x, int y, Orientation orientation)
        {
            if (x < 0)
            {
                throw new ArgumentOutOfRangeException("x");
            }

            if (y < 0)
            {
                throw new ArgumentOutOfRangeException("x");
            }

            this.X = x;
            this.Y = y;
            this.Orientation = orientation;
        }

        public Orientation Orientation { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
