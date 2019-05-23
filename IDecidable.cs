using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    public interface IDecidable : IDuplicable<IDecidable>
    {
        int Decide(List<int> metric);

        List<Double> TheoricalProbability(List<int> metric);
    }
}
