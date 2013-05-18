namespace MartianRobots
{
    /// <summary>
    /// Represents a position on the surface of Mars.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Creates a new position.
        /// </summary>
        /// <param name="x">The x-coordinate of the position.</param>
        /// <param name="y">The y-coordinate of the position.</param>
        /// <param name="orientation">The orientation of the position.</param>
        public Position(int x, int y, Orientation orientation)
        {
            this.X = x;
            this.Y = y;
            this.Orientation = orientation;
        }

        /// <summary>
        /// Gets the orientation of this position.
        /// </summary>
        public Orientation Orientation { get; private set; }

        /// <summary>
        /// Gets the x-coordinate of this position.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Gets the y-coordinate of this position.
        /// </summary>
        public int Y { get; private set; }
    }
}
