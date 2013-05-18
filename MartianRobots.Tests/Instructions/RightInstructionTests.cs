using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MartianRobots.Instructions;

namespace MartianRobots.Tests.Instructions
{
    [TestClass]
    public class RightInstructionShould
    {
        [TestMethod]
        public void HaveTheCorrectCommand()
        {
            var rightInstruction = new RightInstruction();

            Assert.AreEqual('R', rightInstruction.Command);
        }
    }

    [TestClass]
    public class RightInstructionGivenAValidPosition
    {
        private Position validPosition;

        [TestInitialize]
        public void Setup()
        {
            this.validPosition = new Position(0, 0, Orientation.N);
        }

        [TestMethod]
        public void WhenCalledToActThenDoesNotAlterXOrY()
        {
            var rightInstruction = new RightInstruction();

            var newPosition = rightInstruction.Act(this.validPosition);

            Assert.AreEqual(0, newPosition.X);
            Assert.AreEqual(0, newPosition.Y);
        }
    }

    [TestClass]
    public class RightInstructionGivenANorthFacingPosition
    {
        private Position validPosition;

        [TestInitialize]
        public void Setup()
        {
            this.validPosition = new Position(0, 0, Orientation.N);
        }

        [TestMethod]
        public void WhenCalledToActThenReturnsAEastFacingPosition()
        {
            var rightInstruction = new RightInstruction();

            var newPosition = rightInstruction.Act(this.validPosition);

            Assert.AreEqual(Orientation.E, newPosition.Orientation);
        }
    }

    [TestClass]
    public class RightInstructionGivenAEastFacingPosition
    {
        private Position validPosition;

        [TestInitialize]
        public void Setup()
        {
            this.validPosition = new Position(0, 0, Orientation.E);
        }

        [TestMethod]
        public void WhenCalledToActThenReturnsASouthFacingPosition()
        {
            var rightInstruction = new RightInstruction();

            var newPosition = rightInstruction.Act(this.validPosition);

            Assert.AreEqual(Orientation.S, newPosition.Orientation);
        }
    }

    [TestClass]
    public class RightInstructionGivenASouthFacingPosition
    {
        private Position validPosition;

        [TestInitialize]
        public void Setup()
        {
            this.validPosition = new Position(0, 0, Orientation.S);
        }

        [TestMethod]
        public void WhenCalledToActThenReturnsAWestFacingPosition()
        {
            var rightInstruction = new RightInstruction();

            var newPosition = rightInstruction.Act(this.validPosition);

            Assert.AreEqual(Orientation.W, newPosition.Orientation);
        }
    }

    [TestClass]
    public class RightInstructionGivenAWestFacingPosition
    {
        private Position validPosition;

        [TestInitialize]
        public void Setup()
        {
            this.validPosition = new Position(0, 0, Orientation.W);
        }

        [TestMethod]
        public void WhenCalledToActThenReturnsANorthFacingPosition()
        {
            var rightInstruction = new RightInstruction();

            var newPosition = rightInstruction.Act(this.validPosition);

            Assert.AreEqual(Orientation.N, newPosition.Orientation);
        }
    }
}
