using System;
using System.Collections.Generic;
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
            inputLines.RemoveAll(s => s == string.Empty);
            if (inputLines.Count() == 0)
            {
                Console.WriteLine("File is empty.");
                return;
            }

            var firstLine = inputLines.First();

            var instructions = MakeInstructions();
            var grid = BuildGrid(firstLine);
            inputLines.RemoveAt(0);

            var robot = GetRobot(inputLines);

            while (robot != null)
            {
                Console.WriteLine("Robot: {0} {1}", robot.StartPosition, string.Join("-", robot.Instructions.Select(i => i.ToString())));

                robot.Traverse(grid);
                robot = GetRobot(inputLines);
            }
        }

        private static List<Instruction> MakeInstructions()
        {
            var instructions = new List<Instruction>();

            //instructions.Add(new Instruction('L', p =>
            //{

            //}));

            return instructions;
        }

        public static bool[,] BuildGrid(string firstLine)
        {
            var splits = Regex.Split(firstLine, @"\D+");

            var x = int.Parse(splits[0]);
            var y = int.Parse(splits[1]);

            return new bool[x, y];
        }

        public static Robot GetRobot(List<string> lines)
        {
            var firstLine = lines.Take(1).SingleOrDefault();

            if (firstLine == null)
            {
                return null;
            }

            lines.RemoveAt(0);

            var splits = firstLine.Split(' ');

            int x = int.Parse(splits[0]);
            int y = int.Parse(splits[1]);
            Orientation orientation;
            Enum.TryParse<Orientation>(splits[2], false, out orientation);
            var position = new Position(
                int.Parse(splits[0]),
                int.Parse(splits[1]),
                orientation);

            var secondLine = lines.Take(1).Single();
            lines.RemoveAt(0);

            var instructions = new List<Instruction>();
            foreach (var character in secondLine.ToCharArray())
            {
                //Instruction instruction;
                //Enum.TryParse<Instruction>(character.ToString(), false, out instruction);
                //instructions.Add(instruction);
            }

            return new Robot(instructions, position);
        }
    }
}
