namespace MartianRobots.Instructions
{
    /// <summary>
    /// Defines methods to represent an instruction.
    /// </summary>
    public interface IInstruction
    {
        char Command { get; }

        Position Act(Position position);
    }
}
