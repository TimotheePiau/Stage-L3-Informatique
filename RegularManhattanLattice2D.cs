using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    class RegularManhattanLattice2D : ManhattanLattice2D
    {
        //public int xmax { get; private set; }
        //public int ymax { get; private set; }

        public int x0 { get; private set; }
        public int y0 { get; private set; }

        private Random random;

        public RegularManhattanLattice2D(int elementIDNumber, int xmax, int ymax, int x0, int y0)
        {
            elementID = new ID { type = ElementType.Struct, number = elementIDNumber };

            this.xmax = xmax;
            this.ymax = ymax;

            this.x0 = x0;
            this.y0 = y0;
        }

        private int coorToIndex(int coor)
        {
            if (coor < 0)
                return coor * -2;
            else if (coor > 0)
                return coor * 2 - 1;
            else
                return 0;
        }

        private int indexToCoor(int index)
        {
            if (index == 0)
                return 0;
            else if (index % 2 == 0)
                return -index / 2;
            else //if (index % 2 == 1)
                return (index + 1) / 2;
        }

        public override List<int> GetM(Position position)
        {
            List<int> mMetrics = new List<int>();

            if (Math.Abs((position.y + y0) % 2) == 0)
            {
                mMetrics.Add(1);
                mMetrics.Add(0);
            }
            if (Math.Abs((position.y + y0) % 2) == 1)
            {
                mMetrics.Add(0);
                mMetrics.Add(1);
            }
            if (Math.Abs((position.x + x0) % 2) == 0)
            {
                mMetrics.Add(1);
                mMetrics.Add(0);
            }
            if (Math.Abs((position.x + x0) % 2) == 1)
            {
                mMetrics.Add(0);
                mMetrics.Add(1);
            }

            return mMetrics;
        }

        public override Position GetDestination(Position position, int directionNumber)
        {
            Position destination = position;

            switch (directionNumber)
            {
                case 0: position.x += 1; break;
                case 1: position.x += -1; break;
                case 2: position.y += 1; break;
                case 3: position.y += -1; break;
            }

            return position;
        }

        public override Structure2D Duplicate()
        {
            return new RegularManhattanLattice2D(elementID.number, xmax, ymax, x0, y0);
        }
    }
}
