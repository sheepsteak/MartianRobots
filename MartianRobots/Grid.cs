using System;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace MartianRobots
{
    /// <summary>
    /// Represents a <see cref="IGrid"/> for a <see cref="Robot"/> to traverse.
    /// </summary>
    public class Grid : IGrid
    {
        private bool[,] grid;

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

        public void Initialize(string line)
        {
            Contract.Requires(line != null);

            Contract.Requires(!string.IsNullOrWhiteSpace(line));

            var splits = Regex.Split(line, @"\D+");

            if (splits.Length != 2)
            {
                throw new ArgumentException("Line must be formatted like: '5 5'");
            }

            int x;
            if (!int.TryParse(splits[0], out x))
            {
                throw new ArgumentException("Cannot parse X value.");
            }

            int y;
            if (!int.TryParse(splits[1], out y))
            {
                throw new ArgumentException("Cannot parse Y value.");
            }

            this.SizeX = x;
            this.SizeY = y;
            this.grid = new bool[x + 1, y + 1];
        }

        /// <summary>
        /// Attempts to move the <see cref="Robot"/> over the surface of <see cref="Grid"/>.
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
                if (this.grid[currentPosition.X, currentPosition.Y])
                {
                    return RobotFeedback.Scented;
                }
                else
                {
                    this.grid[currentPosition.X, currentPosition.Y] = true;
                    return RobotFeedback.Lost;
                }
            }
        }
    }
}
