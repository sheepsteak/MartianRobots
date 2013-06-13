using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MartianRobots.Tests
{
    [TestClass]
    public class PositionEqual
    {
        [TestMethod]
        public void WhenPassedAnEqualPositionShouldReturnTrue()
        {
            var position1 = new Position(3, 3, Orientation.S);
            var position2 = new Position(3, 3, Orientation.S);

            Assert.AreEqual(position1, position2);
        }

        [TestMethod]
        public void WhenPassedADifferentPositionShouldReturnFalse()
        {
            var position1 = new Position(3, 3, Orientation.S);
            var position2 = new Position(1, 1, Orientation.W);

            Assert.AreNotEqual(position1, position2);
        }
    }
}
