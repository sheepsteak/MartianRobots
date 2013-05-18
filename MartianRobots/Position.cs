using System;

namespace MartianRobots
{
    /// <summary>
    /// Represents a position on the surface of Mars.
    /// </summary>
    public class Position : IEquatable<Position>
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

        /// <summary>
        /// Determines whether the specified <see cref="Position"/> is equal to the current <see cref="Position"/>.
        /// </summary>
        /// <param name="other">The other <see cref="Position"/>.</param>
        /// <returns>Whether or not the instances are equal or not.</returns>
        public bool Equals(Position other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.X == other.X &&
                this.Y == other.Y &&
                this.Orientation == other.Orientation)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="Position"/>.
        /// </summary>
        /// <param name="other">The other <see cref="object"/>.</param>
        /// <returns>Whether or not the instances are equal or not.</returns>
        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            var position = other as Position;
            if (position == null)
            {
                return false;
            }
            else
            {
                return Equals(position);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ this.X ^ this.Y ^ (int)this.Orientation;
        }
    }
}
