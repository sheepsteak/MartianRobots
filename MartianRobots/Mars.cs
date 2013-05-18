using System.Diagnostics.Contracts;

namespace MartianRobots
{
    /// <summary>
    /// Represents a <see cref="IGrid"/> for a <see cref="Robot"/> to traverse.
    /// </summary>
    public class Mars : IGrid
    {
        private bool[,] grid;

        /// <summary>
        /// Creates a new <see cref="Mars"/>(!).
        /// </summary>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        public Mars(int sizeX, int sizeY)
        {
            Contract.Requires(sizeX > 0 && sizeX <= 50);
            Contract.Requires(sizeY > 0 && sizeY <= 50);

            this.SizeX = sizeX;
            this.SizeY = sizeY;
            this.grid = new bool[sizeX, sizeY];
        }

        /// <summary>
        /// Gets the size of the x-dimension.
        /// </summary>
        public int SizeX
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the size of the y-dimension.
        /// </summary>
        public int SizeY
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the position at the specified index.
        /// </summary>
        /// <param name="indexX">The x-coordinate.</param>
        /// <param name="indexY">The y-coordinate.</param>
        /// <returns></returns>
        public bool this[int indexX, int indexY]
        {
            get { return this.grid[indexX, indexY]; }
        }
        
        /// <summary>
        /// Attempts to move the <see cref="Robot"/> over the surface of <see cref="Mars"/>.
        /// </summary>
        /// <param name="currentPosition">The current <see cref="Position"/> of the <see cref="Robot"/></param>
        /// <param name="newPosition">The new <see cref="Position"/> of the <see cref="Robot"/></param>
        /// <returns>Feedback relating to the move.</returns>
        public RobotFeedback Move(Position currentPosition, Position newPosition)
        {
            Contract.Requires(currentPosition != null);
            Contract.Requires(newPosition != null);

            if ((newPosition.X >= 0 && newPosition.X <= this.SizeX) &&
                (newPosition.Y >= 0 && newPosition.Y <= this.SizeY))
            {
                return RobotFeedback.Safe;
            }
            else
            {
                if (this.grid[currentPosition.X - 1, currentPosition.Y - 1])
                {
                    return RobotFeedback.Scented;
                }
                else
                {
                    this.grid[currentPosition.X - 1, currentPosition.Y - 1] = true;
                    return RobotFeedback.Lost;
                }
            }
        }
    }
}
