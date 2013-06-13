using System;

namespace MartianRobots
{
    public interface IRobot
    {
        Position FinishPosition { get; }

        bool Lost { get; }

        Position StartPosition { get; }
        
        void Traverse(IGrid grid);
    }
}
