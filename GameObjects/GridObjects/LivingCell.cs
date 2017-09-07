using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class LivingCell
    {
        private Coordinate myCoordinate;
        private Map myGrid;

        public Coordinate Coordinate
        {
            get { return myCoordinate; }
        }

        internal LivingCell(int x, int y, Map grid)
        {
            myCoordinate = new GameObjects.Coordinate(x, y);
            myGrid = grid;
        }

        public override bool Equals(object obj)
        {
            try
            {
                LivingCell testCell = (LivingCell)obj;

                return (myCoordinate.Equals(testCell.myCoordinate) && myGrid == testCell.myGrid);
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            int hCode = myCoordinate.X ^ myCoordinate.Y ^ myGrid.GetHashCode();
            return base.GetHashCode();
        }
    }
}
