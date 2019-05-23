using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    public abstract class DynamicSituation
    {
        public DynamicManager2D dynamicManager { get; protected set; }
        public Structure2D structure { get; protected set; }
        public List<IDecidable> decisionMakers { get; protected set; }
        public List<Particle2D> particles { get; protected set; }
        public float executionTime { get; protected set; }

        public abstract void Play();
    }
}
