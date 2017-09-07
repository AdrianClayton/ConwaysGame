using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameObjects;

namespace Conway.Tests
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void CellCoordinateEqualsEquivalentCoordinate()
        {
            Map testGrid = new Map();

            testGrid.AddCellAtCoordinate(5, 8);
            LivingCell testCell = testGrid.FindCellAtCoordinate(new Coordinate(5, 8));
            Assert.IsTrue(testCell.Coordinate.Equals(new Coordinate(5, 8)));
        }

        [TestMethod]
        public void CellCoordinateNotEqualsInequivalentCoordinate()
        {
            Map testGrid = new Map();

            testGrid.AddCellAtCoordinate(5, 8);
            LivingCell testCell = testGrid.FindCellAtCoordinate(new Coordinate(5, 8));
            Assert.IsFalse(testCell.Coordinate.Equals(new Coordinate(5, 10)));
        }
        
        [TestMethod]
        public void NumberOfSurroundingLivingCellsIsCorrectInSameGrid()
        {
            Map testGrid = new Map();
            
            testGrid.AddCellAtCoordinate(5, 8);
            testGrid.AddCellAtCoordinate(5, 9);
            testGrid.AddCellAtCoordinate(6, 7);
            LivingCell testCell = testGrid.FindCellAtCoordinate(new Coordinate(5, 8));
            LivingCell testCell2 = testGrid.FindCellAtCoordinate(new Coordinate(5, 9));

            Assert.AreEqual(2, testCell.Coordinate.NumberOfSurroundingLivingCells(testGrid));

            Assert.AreEqual(1, testCell2.Coordinate.NumberOfSurroundingLivingCells(testGrid));
        }

        [TestMethod]
        public void NumberOfSurroundingLivingCellsDoesNotTransferBetweenGrids()
        {
            Map testGrid = new Map();
            Map testGrid2 = new Map();

            testGrid.AddCellAtCoordinate(5, 8);
            testGrid.AddCellAtCoordinate(5, 9);
            testGrid2.AddCellAtCoordinate(6, 7);
            LivingCell testCell = testGrid.FindCellAtCoordinate(new Coordinate(5, 8));
            LivingCell testCell2 = testGrid.FindCellAtCoordinate(new Coordinate(5, 9));

            LivingCell testCell3 = testGrid2.FindCellAtCoordinate(new Coordinate(6, 7));

            Assert.AreEqual(1, testCell.Coordinate.NumberOfSurroundingLivingCells(testGrid));
            Assert.AreEqual(1, testCell2.Coordinate.NumberOfSurroundingLivingCells(testGrid));

            Assert.AreEqual(0, testCell3.Coordinate.NumberOfSurroundingLivingCells(testGrid2));
        }
    }
}
