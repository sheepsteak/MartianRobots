using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MartianRobots.Instructions;

namespace MartianRobots.Tests.Instructions
{
    [TestClass]
    public class LeftInstructionShould
    {
        [TestMethod]
        public void HaveTheCorrectCommand()
        {
            var leftInstruction = new LeftInstruction();

            Assert.AreEqual('L', leftInstruction.Command);
        }
    }

    [TestClass]
    public class LeftInstructionGivenAValidPosition
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
            var leftInstruction = new LeftInstruction();

            var newPosition = leftInstruction.Act(this.validPosition);

            Assert.AreEqual(0, newPosition.X);
            Assert.AreEqual(0, newPosition.Y);
        }
    }

    [TestClass]
    public class LeftInstructionGivenANorthFacingPosition
    {
        private Position validPosition;

        [TestInitialize]
        public void Setup()
        {
            this.validPosition = new Position(0, 0, Orientation.N);
        }

        [TestMethod]
        public void WhenCalledToActThenReturnsAWestFacingPosition()
        {
            var leftInstruction = new LeftInstruction();

            var newPosition = leftInstruction.Act(this.validPosition);

            Assert.AreEqual(Orientation.W, newPosition.Orientation);
        }
    }

    [TestClass]
    public class LeftInstructionGivenAEastFacingPosition
    {
        private Position validPosition;

        [TestInitialize]
        public void Setup()
        {
            this.validPosition = new Position(0, 0, Orientation.E);
        }

        [TestMethod]
        public void WhenCalledToActThenReturnsANorthFacingPosition()
        {
            var leftInstruction = new LeftInstruction();

            var newPosition = leftInstruction.Act(this.validPosition);

            Assert.AreEqual(Orientation.N, newPosition.Orientation);
        }
    }

    [TestClass]
    public class LeftInstructionGivenASouthFacingPosition
    {
        private Position validPosition;

        [TestInitialize]
        public void Setup()
        {
            this.validPosition = new Position(0, 0, Orientation.S);
        }

        [TestMethod]
        public void WhenCalledToActThenReturnsAEastFacingPosition()
        {
            var leftInstruction = new LeftInstruction();

            var newPosition = leftInstruction.Act(this.validPosition);

            Assert.AreEqual(Orientation.E, newPosition.Orientation);
        }
    }

    [TestClass]
    public class LeftInstructionGivenAWestFacingPosition
    {
        private Position validPosition;

        [TestInitialize]
        public void Setup()
        {
            this.validPosition = new Position(0, 0, Orientation.W);
        }

        [TestMethod]
        public void WhenCalledToActThenReturnsASouthFacingPosition()
        {
            var leftInstruction = new LeftInstruction();

            var newPosition = leftInstruction.Act(this.validPosition);

            Assert.AreEqual(Orientation.S, newPosition.Orientation);
        }
    }
}
