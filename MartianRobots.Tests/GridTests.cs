using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MartianRobots.Tests
{
    [TestClass]
    public class GridInitializeShould1
    {
        private Grid grid;
        private string line;

        [TestInitialize]
        public void GivenAValidLine()
        {
            this.grid = new Grid();
            this.line = "10 10";
        }

        [TestMethod]
        public void ShouldHaveTheCorrectDimensions()
        {
            this.grid.Initialize(this.line);

            Assert.AreEqual(10, this.grid.SizeX);
            Assert.AreEqual(10, this.grid.SizeY);
        }
    }

    [TestClass]
    public class GridInitializeShould2
    {
        private Grid grid;
        private string line;

        [TestInitialize]
        public void GivenAnInvalidLine()
        {
            this.grid = new Grid();
            this.line = "1010";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowAnArgumentException()
        {
            this.grid.Initialize(this.line);
        }
    }

    [TestClass]
    public class GridMoveShould
    {
        private Grid grid;

        [TestInitialize]
        public void GivenValidDimensions()
        {
            this.grid = new Grid();
            this.grid.Initialize("1 1");
        }

        [TestMethod]
        public void WhenCheckingANewPositionThatIsOutsideTheGridThenReturnLostFeedback1()
        {
            var currentPosition = new Position(0, 1, Orientation.N);
            var newPosition = new Position(0, 2, Orientation.N);

            var feedback = this.grid.Move(currentPosition, newPosition);

            Assert.AreEqual(RobotFeedback.Lost, feedback);
        }

        [TestMethod]
        public void WhenCheckingANewPositionThatIsOutsideTheGridThenReturnLostFeedback2()
        {
            var currentPosition = new Position(1, 0, Orientation.E);
            var newPosition = new Position(2, 0, Orientation.E);

            var feedback = this.grid.Move(currentPosition, newPosition);

            Assert.AreEqual(RobotFeedback.Lost, feedback);
        }

        [TestMethod]
        public void WhenCheckingANewPositionThatIsOutsideTheGridThenReturnLostFeedback3()
        {
            var currentPosition = new Position(0, 0, Orientation.S);
            var newPosition = new Position(0, -1, Orientation.S);

            var feedback = this.grid.Move(currentPosition, newPosition);

            Assert.AreEqual(RobotFeedback.Lost, feedback);
        }

        [TestMethod]
        public void WhenCheckingANewPositionThatIsOutsideTheGridThenReturnLostFeedback4()
        {
            var currentPosition = new Position(0, 0, Orientation.W);
            var newPosition = new Position(-1, 0, Orientation.W);

            var feedback = this.grid.Move(currentPosition, newPosition);

            Assert.AreEqual(RobotFeedback.Lost, feedback);
        }

        [TestMethod]
        public void WhenCheckingANewPositionThatIsInsideTheGridThenReturnSafeFeedback()
        {
            var currentPosition = new Position(0, 0, Orientation.E);
            var newPosition = new Position(1, 0, Orientation.E);

            var feedback = this.grid.Move(currentPosition, newPosition);

            Assert.AreEqual(RobotFeedback.Safe, feedback);
        }

        [TestMethod]
        public void WhenCheckingANewPositionThatOutsideTheGridThatAPreviousRobotWasLostAtThenReturnScentedFeedback()
        {
            var currentPosition = new Position(1, 0, Orientation.E);
            var newPosition = new Position(2, 0, Orientation.E);

            var lostFeedback = this.grid.Move(currentPosition, newPosition);

            Assert.AreEqual(RobotFeedback.Lost, lostFeedback);

            var scentedFeedback = this.grid.Move(currentPosition, newPosition);

            Assert.AreEqual(RobotFeedback.Scented, scentedFeedback);
        }
    }
}
