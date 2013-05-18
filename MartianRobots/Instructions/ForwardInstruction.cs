using System.Diagnostics.Contracts;

namespace MartianRobots.Instructions
{
    /// <summary>
    /// Represents an <see cref="Instruction"/> that moves a robot in the direction it's facing.
    /// </summary>
    public class ForwardInstruction : Instruction
    {
        private static char command = 'F';

        /// <summary>
        /// Gets the command that links to this <see cref="Instruction"/>.
        /// </summary>
        public override char Command
        {
            get { return command; }
        }

        /// <summary>
        /// Takes a position and applies the <see cref="ForwardInstruction"/> to it.
        /// </summary>
        /// <param name="position">A position to transform.</param>
        /// <returns>The result of applying the <see cref="ForwardInstruction"/>.</returns>
        public override Position TransformPosition(Position position)
        {
            Contract.Requires(position != null);
            Contract.Ensures(Contract.Result<Position>() != null);

            switch (position.Orientation)
            {
                case Orientation.N:
                    return new Position(position.X, position.Y + 1, position.Orientation);
                case Orientation.S:
                    return new Position(position.X, position.Y - 1, position.Orientation);
                case Orientation.W:
                    return new Position(position.X - 1, position.Y, position.Orientation);
                case Orientation.E:
                    return new Position(position.X + 1, position.Y, position.Orientation);
                default:
                    return new Position(position.X, position.Y, position.Orientation);
            }
        }
    }
}
