using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    public abstract class ManhattanLattice2D : Structure2D
    {
        public int xmax { get; protected set; }
        public int ymax { get; protected set; }
    }
}
