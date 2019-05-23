using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    class WalkerFactory : IParticleFactory
    {
        public List<Position> particlesPositions { get; private set; }
        public List<float> dts { get; private set; }

        public WalkerFactory(List<Position> particlesPositions, List<float> dts = null)
        {
            this.particlesPositions = particlesPositions;
            this.dts = dts;
            if (dts != null && particlesPositions.Count != dts.Count)
                throw new Exception("Particles position count must match delta Time count");
        }

        public List<Particle2D> CreateParticles(Structure2D structure, List<IDecidable> decisionMakers)
        {
            if(decisionMakers.Count != particlesPositions.Count)
                throw new Exception("Particles position count must match delta Time count");

            List<Particle2D> newParticles = new List<Particle2D>();

            if (dts == null)
            {
                for (int i = 0; i < particlesPositions.Count; i++)
                {
                    newParticles.Add(new Walker2D(i, structure, decisionMakers[i], particlesPositions[i]));
                }
            }
            else
            {
                for(int i = 0; i < particlesPositions.Count; i++)
                {
                    newParticles.Add(new Walker2D(i, structure, decisionMakers[i], particlesPositions[i], dts[i]));
                }
            }

            return newParticles;
        }

    }
}
