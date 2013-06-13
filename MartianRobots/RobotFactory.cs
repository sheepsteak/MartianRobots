using MartianRobots.Instructions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace MartianRobots
{
    public class RobotFactory : MartianRobots.IRobotFactory
    {
        private IEnumerable<Instruction> instructions;

        public RobotFactory(IEnumerable<Instruction> instructions)
        {
            Contract.Requires(instructions != null);

            this.instructions = instructions;
        }

        public IEnumerable<IRobot> CreateRobots(IEnumerable<string> lines)
        {
            Contract.Requires(lines != null);
            Contract.Ensures(Contract.Result<IEnumerable<Robot>>() != null);

            var robots = new List<Robot>();
            var linesEnumerator = lines.GetEnumerator();

            while (linesEnumerator.MoveNext())
            {
                if (string.IsNullOrWhiteSpace(linesEnumerator.Current))
                {
                    continue;
                }

                // First line - position
                var splits = linesEnumerator.Current.Split(' ');

                int x;
                if (!int.TryParse(splits[0], out x))
                {
                    throw new ArgumentException("Could not retrieve robot's X position!");
                }

                int y;
                if (!int.TryParse(splits[1], out y))
                {
                    throw new ArgumentException("Could not retrieve robot's Y position!");
                }

                Orientation orientation;
                if (!Enum.TryParse<Orientation>(splits[2], false, out orientation))
                {
                    throw new ArgumentException("Could not retrieve robot's orientation!");
                }

                var position = new Position(
                    int.Parse(splits[0]),
                    int.Parse(splits[1]),
                    orientation);

                // Move to second line
                if (!linesEnumerator.MoveNext())
                {
                    throw new ArgumentException("Expecting robot's instructions!");
                }

                //Second line
                var robotInstructions = new List<Instruction>();
                foreach (var character in linesEnumerator.Current.ToCharArray())
                {
                    var instruction = this.instructions
                        .FirstOrDefault(i => i.Command == char.ToUpperInvariant(character));

                    if (instruction == null)
                    {
                        throw new ArgumentException("Input contains an unregistered or invalid instruction.");
                    }

                    robotInstructions.Add(instruction);
                }

                robots.Add(new Robot(robotInstructions, position));
            }

            return robots;
        }
    }
}
