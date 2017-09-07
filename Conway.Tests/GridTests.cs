using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameObjects;

namespace Conway.Tests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void CellsCanBeAdded()
        {
            Map testGrid = new Map();
            testGrid.AddCellAtCoordinate(-3, 0);
            Assert.AreEqual(1, testGrid.LivingCells.Count);
        }


        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CellsCannotBeAddedOverExistingCells()
        {
            Map testGrid = new Map();
            testGrid.AddCellAtCoordinate(4, 5);
            try
            {
                testGrid.AddCellAtCoordinate(4, 5);
            }
            catch (ApplicationException ex)
            {
                Assert.AreEqual("There shouldn't be any need to add another cell to Coordinate x: 4, y: 5. there is already one", ex.Message);
                throw ex;
            }
        }

        [TestMethod]
        public void CellsRemovedWhereThereAreCells()
        {
            Map testGrid = new Map();
            testGrid.AddCellAtCoordinate(4, 57);

            Assert.IsTrue(testGrid.RemoveCellAtCoordinate(new Coordinate(4, 57)));

        }

        [TestMethod]
        public void CellsCannotBeRemovedWhereThereAreNoCells()
        {
            Map testGrid = new Map();
            testGrid.AddCellAtCoordinate(7, 2);

            Assert.IsFalse(testGrid.RemoveCellAtCoordinate(new Coordinate(3, 5)));
            
        }
    }
}
