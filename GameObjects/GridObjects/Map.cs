using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class Map
    {

        private Dictionary<Coordinate, LivingCell> _livingCells = new Dictionary<Coordinate, LivingCell>();

        public Dictionary<Coordinate, LivingCell> LivingCells
        {
            get { return _livingCells; }
        }

        public void AddCellAtCoordinate(int x, int y)
        {
            LivingCell cellToAdd = new LivingCell(x, y, this);
            LivingCell ExistingCell = FindCellAtCoordinate(cellToAdd.Coordinate);
            if (ExistingCell == null)
            {
                _livingCells.Add(cellToAdd.Coordinate, cellToAdd);
            }
            else
            {
                throw new ApplicationException(string.Format("There shouldn't be any need to add another cell to Coordinate x: {0}, y: {1}. there is already one", x, y));
            }
        }

        public void AddCellAtCoordinate(Coordinate coordinate)
        {
            AddCellAtCoordinate(coordinate.X, coordinate.Y);
        }

        public bool RemoveCellAtCoordinate(Coordinate deadCellCoordinate)
        {
            return _livingCells.Remove(deadCellCoordinate);
        }

        public bool RemoveCell(LivingCell cell)
        {
            return RemoveCellAtCoordinate(cell.Coordinate);
        }

        public LivingCell FindCellAtCoordinate(Coordinate coordinate)
        {
            LivingCell existingCell;
            _livingCells.TryGetValue(coordinate, out existingCell);

            return existingCell;
        }
    }
}
