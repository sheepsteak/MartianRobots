using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MartianRobots.Instructions;

namespace MartianRobots.Tests.Instructions
{
    [TestClass]
    public class ForwardInstructionShould
    {
        [TestMethod]
        public void HaveTheCorrectCommand()
        {
            var forwardInstruction = new ForwardInstruction();

            Assert.AreEqual('F', forwardInstruction.Command);
        }
    }

    [TestClass]
    public class ForwardInstructionGivenAValidPosition
    {
        private Position validPosition;

        [TestInitialize]
        public void Setup()
        {
            this.validPosition = new Position(0, 0, Orientation.N);
        }

        [TestMethod]
        public void WhenCalledToActThenDoesNotAlterOrientation()
        {
            var forwardInstruction = new ForwardInstruction();

            var newPosition = forwardInstruction.TransformPosition(this.validPosition);

            Assert.AreEqual(this.validPosition.Orientation, newPosition.Orientation);
        }
    }

    [TestClass]
    public class ForwardInstructionGivenANorthFacingPosition
    {
        private Position validPosition;

        [TestInitialize]
        public void Setup()
        {
            this.validPosition = new Position(0, 0, Orientation.N);
        }

        [TestMethod]
        public void WhenCalledToActThenReturnsAPositionHigherOnGrid()
        {
            var forwardInstruction = new ForwardInstruction();

            var newPosition = forwardInstruction.TransformPosition(this.validPosition);

            Assert.AreEqual(0, newPosition.X);
            Assert.AreEqual(1, newPosition.Y);
        }
    }

    [TestClass]
    public class ForwardInstructionGivenAEastFacingPosition
    {
        private Position validPosition;

        [TestInitialize]
        public void Setup()
        {
            this.validPosition = new Position(0, 0, Orientation.E);
        }

        [TestMethod]
        public void WhenCalledToActThenReturnsAPositionOneToTheRight()
        {
            var forwardInstruction = new ForwardInstruction();

            var newPosition = forwardInstruction.TransformPosition(this.validPosition);

            Assert.AreEqual(1, newPosition.X);
            Assert.AreEqual(0, newPosition.Y);
        }
    }

    [TestClass]
    public class ForwardInstructionGivenASouthFacingPosition
    {
        private Position validPosition;

        [TestInitialize]
        public void Setup()
        {
            this.validPosition = new Position(1, 1, Orientation.S);
        }

        [TestMethod]
        public void WhenCalledToActThenReturnsPositionLowerOnGrid()
        {
            var forwardInstruction = new ForwardInstruction();

            var newPosition = forwardInstruction.TransformPosition(this.validPosition);

            Assert.AreEqual(1, newPosition.X);
            Assert.AreEqual(0, newPosition.Y);
        }
    }

    [TestClass]
    public class ForwardInstructionGivenAWestFacingPosition
    {
        private Position validPosition;

        [TestInitialize]
        public void Setup()
        {
            this.validPosition = new Position(1, 1, Orientation.W);
        }

        [TestMethod]
        public void WhenCalledToActThenReturnsAPositionToTheLeft()
        {
            var forwardInstruction = new ForwardInstruction();

            var newPosition = forwardInstruction.TransformPosition(this.validPosition);

            Assert.AreEqual(0, newPosition.X);
            Assert.AreEqual(1, newPosition.Y);
        }
    }
}
