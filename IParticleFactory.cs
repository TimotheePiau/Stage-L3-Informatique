using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    interface IParticleFactory
    {
        List<Particle2D> CreateParticles(Structure2D structure, List<IDecidable> decisionMakers);
    }
}
