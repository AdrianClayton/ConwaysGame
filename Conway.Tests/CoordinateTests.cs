using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameObjects;

namespace Conway.Tests
{
    [TestClass]
    public class CoordinateTests
    {
        [TestMethod]
        public void CoordinateEqualsEquivalentCoordinate()
        {
            Coordinate thisCoordinate = new Coordinate(4, 5);
            Coordinate otherCoordinate = new Coordinate(4, 5);
            Assert.IsTrue(thisCoordinate.Equals(otherCoordinate));
        }

        [TestMethod]
        public void CoordinateNotEqualsInequivalentCoordinate()
        {
            Coordinate thisCoordinate = new Coordinate(4, 7);
            Coordinate otherCoordinate = new Coordinate(0, 5);
            Assert.IsFalse(thisCoordinate.Equals(otherCoordinate));
        }
    }
}
