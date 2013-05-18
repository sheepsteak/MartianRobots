namespace MartianRobots.Instructions
{
    /// <summary>
    /// Defines methods to represent an instruction.
    /// </summary>
    public abstract class Instruction
    {
        /// <summary>
        /// Gets the command that links to this <see cref="Instruction"/>.
        /// </summary>
        public abstract char Command { get; }

        /// <summary>
        /// Takes a position and applies the <see cref="Instruction"/> to it.
        /// </summary>
        /// <param name="position">A position to transform.</param>
        /// <returns>The result of applying the <see cref="Instruction"/>.</returns>
        public abstract Position TransformPosition(Position position);
    }
}
