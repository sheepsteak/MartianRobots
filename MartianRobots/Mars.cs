using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace MartianRobots
{
    public class Mars
    {
        private IGrid grid;
        private IRobotFactory robotFactory;

        public Mars(IGrid grid, IRobotFactory robotFactory)
        {
            Contract.Requires(grid != null);
            Contract.Requires(robotFactory != null);

            this.grid = grid;
            this.robotFactory = robotFactory;
        }

        public IEnumerable<string> Run(IEnumerable<string> inputLines)
        {
            Contract.Requires(inputLines != null && inputLines.Count() > 0);

            this.grid.Initialize(inputLines.First());

            IEnumerable<IRobot> robots;
            try
            {
                robots = this.robotFactory.CreateRobots(inputLines.Skip(1));
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("There was an error parsing the input.", e);
            }
          
            var outputLines = new List<string>();

            foreach (var robot in robots)
            {
                robot.Traverse(this.grid);
                var positionString = string.Format("{0} {1} {2}",
                    robot.FinishPosition.X,
                    robot.FinishPosition.Y,
                    robot.FinishPosition.Orientation);

                if (robot.Lost)
                {
                    positionString += " LOST";
                }

                outputLines.Add(positionString);
            }

            return outputLines;
        }
    }
}
