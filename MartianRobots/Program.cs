using MartianRobots.Instructions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
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

            if (inputLines.Count() == 0)
            {
                Console.WriteLine("File is empty.");
                return;
            }

            var firstLine = inputLines.First();
            var gridSize = GetGridSize(firstLine);
            var instructions = RegisterInstructions();

            var mars = new Mars(gridSize.Item1, gridSize.Item2);

            var robots = GetRobots(inputLines.Skip(1), instructions);

            foreach (var robot in robots)
            {
                robot.Traverse(mars);
                var positionString = string.Format("{0} {1} {2}",
                    robot.FinishPosition.X,
                    robot.FinishPosition.Y,
                    robot.FinishPosition.Orientation);

                Console.Write(positionString);

                if (robot.Lost)
                {
                    var foregroundTemp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" LOST");
                    Console.ForegroundColor = foregroundTemp;
                }
                else
                {
                    Console.Write(Environment.NewLine);
                }
            }
        }

        public static IEnumerable<Instruction> RegisterInstructions()
        {
            Contract.Ensures(Contract.Result<IEnumerable<Instruction>>() != null);

            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Instruction)))
                .Select(t => Activator.CreateInstance(t))
                .Cast<Instruction>();
        }

        public static Tuple<int, int> GetGridSize(string line)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(line));
            Contract.Ensures(Contract.Result<Tuple<int, int>>() != null);

            var splits = Regex.Split(line, @"\D+");

            int x;
            if (!int.TryParse(splits[0], out x))
            {
                throw new ArgumentException("Cannot parse X value.");
            }

            int y;
            if (!int.TryParse(splits[1], out y))
            {
                throw new ArgumentException("Cannot parse Y value.");
            }

            return new Tuple<int, int>(x, y);
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
    }
}
