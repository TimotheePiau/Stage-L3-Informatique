using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    class RandomManhattanLattice2D : ManhattanLattice2D
    {
        //public int xmax { get; private set; }
        //public int ymax { get; private set; }

        public List<int> LinesDirections { get; private set; }
        public List<int> ColumnsDirections { get; private set; }

        private Random random;

        public RandomManhattanLattice2D(int elementIDNumber, int xmax, int ymax, Random random=null)
        {
            elementID = new ID { type = ElementType.Struct, number = elementIDNumber };

            this.xmax = xmax;
            this.ymax = ymax;

            if (random == null)
                this.random = new Random();
            else
                this.random = random;

            LinesDirections = new List<int>();
            ColumnsDirections = new List<int>();

            setDirections();
        }

        public RandomManhattanLattice2D(int elementIDNumber, int xmax, int ymax, List<int> linesDirection, List<int> columnsDirection)
        {
            elementID = new ID { type = ElementType.Struct, number = elementIDNumber };

            this.xmax = xmax;
            this.ymax = ymax;

            this.LinesDirections = linesDirection;
            this.ColumnsDirections = columnsDirection;
        }

        private void setDirections()
        {
            for(int x = 0; x <= 2 * ymax + 1; x++)
            {
                LinesDirections.Add(pickRandomDirection());
            }

            for(int y = 0; y <= 2 * xmax +1; y++)
            {
                ColumnsDirections.Add(pickRandomDirection());
            }
        }

        private int pickRandomDirection()
        {
            if (random.Next(0, 2) == 0)
                return -1;
            return 1;
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
            List <int> mMetrics = new List<int>();

            if (LinesDirections[coorToIndex(position.y)] == 1)
            {
                mMetrics.Add(1);
                mMetrics.Add(0);
            }
            if(LinesDirections[coorToIndex(position.y)] == -1)
            {
                mMetrics.Add(0);
                mMetrics.Add(1);
            }
            if(ColumnsDirections[coorToIndex(position.x)] == 1)
            {
                mMetrics.Add(1);
                mMetrics.Add(0);
            }
            if(ColumnsDirections[coorToIndex(position.x)] == -1)
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
            List<int> duplicateLinesDirections = new List<int>();
            List<int> duplicateColunmsDirections = new List<int>();

            foreach(int lineDirection in LinesDirections)
            {
                duplicateLinesDirections.Add(lineDirection);
            }
            
            foreach(int colunmDirection in ColumnsDirections)
            {
                duplicateColunmsDirections.Add(colunmDirection);
            }

            return new RandomManhattanLattice2D(elementID.number, xmax, ymax, duplicateLinesDirections, duplicateColunmsDirections);
        }
    }
}
