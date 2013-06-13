using Autofac;
using MartianRobots.Instructions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;

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

            var filename = args[0];

            if (!File.Exists(filename))
            {
                Console.WriteLine("Please provide a valid input file.");
                return;
            }

            var inputLines = File.ReadAllLines(filename).ToList();

            if (inputLines.Count() == 0)
            {
                Console.WriteLine("File is empty.");
                return;
            }

            var container = ConfigureContainer();

            var mars = container.Resolve<Mars>();
            IEnumerable<string> resultLines = null;
            try
            {
                resultLines = mars.Run(inputLines);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            if (resultLines != null)
            {
                foreach (var resultLine in resultLines)
                {
                    Console.WriteLine(resultLine);
                }
            }
        }

        public static IContainer ConfigureContainer()
        {
            Contract.Ensures(Contract.Result<IContainer>() != null);

            var container = new ContainerBuilder();
            container.RegisterType<Mars>();
            container.RegisterType<Grid>().As<IGrid>();
            container.RegisterType<RobotFactory>().As<IRobotFactory>();
            container.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Instruction"))
                .As<Instruction>();

            return container.Build();
        }
    }
}
