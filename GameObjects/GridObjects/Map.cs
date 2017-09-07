using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class Map
    {

        private List<LivingCell> _livingCells = new List<LivingCell>();

        public List<LivingCell> LivingCells
        {
            get { return _livingCells; }
        }

        public void AddCellAtCoordinate(int x, int y)
        {
            LivingCell cellToAdd = new LivingCell(x, y, this);
            LivingCell ExistingCell = FindCellAtCoordinate(cellToAdd.Coordinate);
            if (ExistingCell == null)
            {
                LivingCells.Add(cellToAdd);
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
            LivingCell deadCell = FindCellAtCoordinate(deadCellCoordinate);
            return LivingCells.Remove(deadCell);
        }

        public bool RemoveCell(LivingCell cell)
        {
            return RemoveCellAtCoordinate(cell.Coordinate);
        }

        public LivingCell FindCellAtCoordinate(Coordinate coordinate)
        {
            List<LivingCell> ExistingCells = (List<LivingCell>)LivingCells.Where(z => z.Coordinate.Equals(coordinate)).ToList(); ;
            if (ExistingCells.Count > 1)
            {
                throw new ApplicationException(string.Format("There shouldn't be more than one cell at Coordinate x: {0}, y: {1}", coordinate.X, coordinate.Y));
            }
            else if (ExistingCells.Count == 1)
            {
                return ExistingCells[0];
            }
            else return null;
        }
    }
}
