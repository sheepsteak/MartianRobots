using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MartianRobots.Tests
{
    [TestClass]
    public class PositionEqualGivenAnEqualPosition
    {
        [TestMethod]
        public void ShouldReturnTrue()
        {
            var position1 = new Position(3, 3, Orientation.S);
            var position2 = new Position(3, 3, Orientation.S);

            Assert.AreEqual(position1, position2);
        }
    }
}
