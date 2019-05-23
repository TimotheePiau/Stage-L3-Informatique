using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    interface ISituationFactory
    {
        DynamicSituation CreateSituation();
    }
}
