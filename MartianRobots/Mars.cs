using MartianRobots.Instructions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MartianRobots
{
    public class Mars
    {
        private IGrid grid;
        private IEnumerable<string> inputLines;
        private IEnumerable<Instruction> instructions;

        public Mars(IEnumerable<string> inputLines, IGrid grid, IEnumerable<Instruction> instructions)
        {
            Contract.Requires(inputLines != null);
            Contract.Requires(grid != null);
            Contract.Requires(instructions != null);

            this.inputLines = inputLines;
            this.grid = grid;
            this.instructions = instructions;
        }

        public static IEnumerable<Robot> GetRobots(IEnumerable<string> lines, IEnumerable<Instruction> instructions)
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

                int x = int.Parse(splits[0]);
                int y = int.Parse(splits[1]);
                Orientation orientation;
                Enum.TryParse<Orientation>(splits[2], false, out orientation);
                var position = new Position(
                    int.Parse(splits[0]),
                    int.Parse(splits[1]),
                    orientation);

                // Move to second line
                if (!linesEnumerator.MoveNext())
                {
                    throw new InvalidOperationException("Expecting a second line!");
                }

                //Second line
                var robotInstructions = new List<Instruction>();
                foreach (var character in linesEnumerator.Current.ToCharArray())
                {
                    var instruction = instructions
                        .First(i => i.Command == char.ToUpperInvariant(character));

                    robotInstructions.Add(instruction);
                }

                robots.Add(new Robot(robotInstructions, position));
            }

            return robots;
        }


        public IEnumerable<string> Run()
        {
            var robots = GetRobots(inputLines.Skip(1), instructions);
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
                    positionString +=" LOST";
                }

                outputLines.Add(positionString);
            }

            return outputLines;
        }
    }
}
