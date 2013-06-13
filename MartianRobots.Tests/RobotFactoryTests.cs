using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MartianRobots.Instructions;
using System.Collections.Generic;

namespace MartianRobots.Tests
{
    [TestClass]
    public class RobotFactoryCreateRobotsShould
    {
        private RobotFactory robotFactory;

        [TestInitialize]
        public void GivenValidInstructions()
        {
            var instructions = new List<Instruction>()
            {
                new ForwardInstruction(),
                new LeftInstruction(),
                new RightInstruction()
            };

            this.robotFactory = new RobotFactory(instructions);
        }

        [TestMethod]
        public void WhenPassedValidInputLinesThenCreateARobot()
        {
            var inputLines = new[] { "1 1 E", "RFRFRFRF" };

            var robots = this.robotFactory.CreateRobots(inputLines);

            Assert.AreEqual(1, robots.Count());

            var robot = robots.First();
            var expectedPosition = new Position(1, 1, Orientation.E);

            Assert.AreEqual(expectedPosition, robot.StartPosition);
        }

        [TestMethod]
        public void WhenPassedValidInputLinesThenCreateTwoRobots()
        {
            var inputLines = new[] { "1 1 E", "RFRFRFRF", "2 4 S", "LRFFLR" };

            var robots = this.robotFactory.CreateRobots(inputLines);

            Assert.AreEqual(2, robots.Count());

            var robot = robots.ElementAt(0);
            var expectedPosition = new Position(1, 1, Orientation.E);

            Assert.AreEqual(expectedPosition, robot.StartPosition);

            robot = robots.ElementAt(1);
            expectedPosition = new Position(2, 4, Orientation.S);

            Assert.AreEqual(expectedPosition, robot.StartPosition);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenPassedInvalidPositionThenThrowException()
        {
            var inputLines = new[] { "1blahE", "RRFRF" };

            var robots = this.robotFactory.CreateRobots(inputLines);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenPassedInvalidInstructionListThenThrowException()
        {
            var inputLines = new[] { "1 2 E", "RFblah F" };

            var robots = this.robotFactory.CreateRobots(inputLines);
        }
    }
}
