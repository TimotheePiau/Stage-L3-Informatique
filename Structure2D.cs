using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    public abstract class Structure2D : IDuplicable<Structure2D>
    {
        public ID elementID { get; protected set; }

        public abstract List<int> GetM(Position position);
        public abstract Position GetDestination(Position position, int directionNumber);

        public abstract Structure2D Duplicate();
    }
}
