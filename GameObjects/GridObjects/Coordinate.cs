using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class Coordinate :IEquatable<Coordinate>
    {
        public int X { get; }
        public int Y { get; }

        public bool Equals(Coordinate other)
        {
            bool xtrue = (X == other.X);
            bool ytrue = (Y == other.Y);
            return xtrue && ytrue;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coordinate[] SurroundingCoordinates()
        {
            List<Coordinate> neighbors = new List<Coordinate>();

            for(int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == j && i == 0)
                        continue;
                    neighbors.Add(new Coordinate(X + i, Y + j));
                }
            }

            return neighbors.ToArray();
        }

        public int NumberOfSurroundingLivingCells(Map grid)
        {
            Coordinate[] surroundingCoordinates = SurroundingCoordinates();
            int number = 0;

            foreach (Coordinate coordinate in surroundingCoordinates)
            {
                LivingCell testCell = grid.FindCellAtCoordinate(coordinate);
                if (testCell != null)
                {
                    number++;
                }

            }
            return number;
        }
    }
}
