namespace MartianRobots
{
    public interface IGrid
    {
        int SizeX { get; }

        int SizeY { get; }

        bool this[int indexX, int indexY] { get; }

        RobotFeedback Move(Position currentPosition, Position newPosition);

        void Initialize(string line);
    }
}
