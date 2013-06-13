using MartianRobots.Instructions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

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

            var grid = GetGrid(inputLines.First());
            var instructions = GetInstructions();

            var mars = new Mars(inputLines, grid, instructions);
            var resultLines = mars.Run();

            foreach (var resultLine in resultLines)
            {
                Console.WriteLine(resultLine);
            }
        }

        public static IGrid GetGrid(string line)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(line));
            Contract.Ensures(Contract.Result<IGrid>() != null);

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

            return new Grid(x, y);
        }

        private static IEnumerable<Instruction> GetInstructions()
        {
            Contract.Ensures(Contract.Result<IEnumerable<Instruction>>() != null);

            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Instruction)))
                .Select(t => Activator.CreateInstance(t))
                .Cast<Instruction>();
        }
    }
}
