using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    public abstract class TheoricalParticle2D : Particle2D, IDuplicable<TheoricalParticle2D>
    {
        public DynamicManager2D dynamicManager { get; protected set; }
        public double probability { get; protected set; }

        public abstract TheoricalParticle2D Duplicate();
        public abstract void UpdateProbability(double moveProbability);
    }
}
