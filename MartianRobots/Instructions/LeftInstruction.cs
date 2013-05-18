using System.Diagnostics.Contracts;

namespace MartianRobots.Instructions
{
    /// <summary>
    /// Represents an <see cref="Instruction"/> that rotates a robot to the left.
    /// </summary>
    public class LeftInstruction : Instruction
    {
        private static char command = 'L';

        /// <summary>
        /// Gets the command that links to this <see cref="Instruction"/>.
        /// </summary>
        public override char Command
        {
            get { return command; }
        }

        /// <summary>
        /// Takes a position and applies the <see cref="LeftInstruction"/> to it.
        /// </summary>
        /// <param name="position">A position to transform.</param>
        /// <returns>The result of applying the <see cref="LeftInstruction"/>.</returns>
        public override Position TransformPosition(Position position)
        {
            Contract.Requires(position != null);
            Contract.Ensures(Contract.Result<Position>() != null);

            switch (position.Orientation)
            {
                case Orientation.N:
                    return new Position(position.X, position.Y, Orientation.W);
                case Orientation.S:
                    return new Position(position.X, position.Y, Orientation.E);
                case Orientation.W:
                    return new Position(position.X, position.Y, Orientation.S);
                case Orientation.E:
                    return new Position(position.X, position.Y, Orientation.N);
                default:
                    return new Position(position.X, position.Y, position.Orientation);
            }
        }
    }
}
