using System;
using System.Collections.Generic;

namespace MartianRobots
{
    public interface IRobotFactory
    {
        IEnumerable<IRobot> CreateRobots(IEnumerable<string> lines);
    }
}
