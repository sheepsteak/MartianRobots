using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MartianRobots
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("Please provide an input file.");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Please provide a valid input file.");
                return;
            }

            var inputLines = File.ReadAllLines(args[0]).ToList();
            //inputLines.RemoveAll(s => s == string.Empty);
            if (inputLines.Count() == 0)
            {
                Console.WriteLine("File is empty.");
                return;
            }

            var firstLine = inputLines.First();
            var gridSize = GetGridSize(firstLine);
            var instructions = MakeInstructions();

            var mars = new Mars(gridSize.Item1, gridSize.Item2, instructions);

            var robots = GetRobots(inputLines.Skip(1), instructions);

            foreach (var robot in robots)
            {
                robot.Traverse(mars);
                var outputString = string.Format("{0} {1} {2}{3}",
                    robot.FinishPosition.X,
                    robot.FinishPosition.Y,
                    robot.FinishPosition.Orientation,
                    robot.Lost ? " LOST" : string.Empty);

                Console.WriteLine(outputString);
            }
        }

        public static IEnumerable<Instruction> MakeInstructions()
        {
            Contract.Ensures(Contract.Result<IEnumerable<Instruction>>() != null);

            var instructions = new List<Instruction>();

            instructions.Add(new Instruction('L', p =>
            {
                switch (p.Orientation)
                {
                    case Orientation.N:
                        return new Position(p.X, p.Y, Orientation.W);
                    case Orientation.S:
                        return new Position(p.X, p.Y, Orientation.E);
                    case Orientation.W:
                        return new Position(p.X, p.Y, Orientation.S);
                    case Orientation.E:
                        return new Position(p.X, p.Y, Orientation.N);
                    default:
                        return new Position(p.X, p.Y, p.Orientation);
                }
            }));

            instructions.Add(new Instruction('R', p =>
            {
                switch (p.Orientation)
                {
                    case Orientation.N:
                        return new Position(p.X, p.Y, Orientation.E);
                    case Orientation.S:
                        return new Position(p.X, p.Y, Orientation.W);
                    case Orientation.W:
                        return new Position(p.X, p.Y, Orientation.N);
                    case Orientation.E:
                        return new Position(p.X, p.Y, Orientation.S);
                    default:
                        return new Position(p.X, p.Y, p.Orientation);
                }
            }));

            instructions.Add(new Instruction('F', p =>
            {
                switch (p.Orientation)
                {
                    case Orientation.N:
                        return new Position(p.X, p.Y + 1, p.Orientation);
                    case Orientation.S:
                        return new Position(p.X, p.Y - 1, p.Orientation);
                    case Orientation.W:
                        return new Position(p.X - 1, p.Y, p.Orientation);
                    case Orientation.E:
                        return new Position(p.X + 1, p.Y, p.Orientation);
                    default:
                        return new Position(p.X, p.Y, p.Orientation);
                }
            }));

            return instructions;
        }

        public static Tuple<int, int> GetGridSize(string line)
        {
            Contract.Requires(line != null);

            var splits = Regex.Split(line, @"\D+");

            var x = int.Parse(splits[0]);
            var y = int.Parse(splits[1]);

            return new Tuple<int, int>(x, y);
        }

        public static IEnumerable<Robot> GetRobots(IEnumerable<string> lines, IEnumerable<Instruction> instructions)
        {
            Contract.Requires(lines != null);

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
                        .First(i => i.Letter == char.ToUpperInvariant(character));

                    robotInstructions.Add(instruction);
                }

                robots.Add(new Robot(robotInstructions, position));
            }

            return robots;
        }
    }
}
