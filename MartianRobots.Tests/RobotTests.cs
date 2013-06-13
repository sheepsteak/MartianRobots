using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MartianRobots.Instructions;
using System.Collections.Generic;

namespace MartianRobots.Tests
{
    [TestClass]
    public class RobotGivenValidInstructionsAndStartPosition
    {
        private IGrid grid;
        private IEnumerable<Instruction> validInstructions;
        private Position validStartPosition;

        [TestInitialize]
        public void Setup()
        {
            this.grid = new Grid(10, 10);

            this.validInstructions = new List<Instruction>()
            {
                new ForwardInstruction(),
                new ForwardInstruction(),
                new LeftInstruction(),
                new RightInstruction(),
                new RightInstruction()
           };

            this.validStartPosition = new Position(1, 0, Orientation.N);
        }

        [TestMethod]
        public void WhenToldToMoveThenEndsUpInExpectedPosition()
        {
            var robot = new Robot(this.validInstructions, this.validStartPosition);

            robot.Traverse(this.grid);

            var expectedPosition = new Position(1, 2, Orientation.E);
            Assert.AreEqual(expectedPosition, robot.FinishPosition);
            Assert.IsFalse(robot.Lost);
        }
    }
}
