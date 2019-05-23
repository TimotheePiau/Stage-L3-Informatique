using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    class TheoricalSituation : DynamicSituation
    {
        public TheoricalSituation(DynamicManager2D dynamicManager, Structure2D structure, List<IDecidable> decisionMakers, List<Particle2D> particles, float executionTime)
        {
            this.dynamicManager = dynamicManager;
            this.structure = structure;
            this.decisionMakers = decisionMakers;
            this.particles = particles;
            this.executionTime = executionTime;

            setup();
        }

        private void setup()
        {
            if (structure is IDynamic)
                dynamicManager.Add((IDynamic)structure);

            foreach (IDecidable decisionMaker in decisionMakers)
            {
                if (decisionMaker is IDynamic)
                    dynamicManager.Add((IDynamic)decisionMaker);
            }

            foreach (Particle2D particle in particles)
            {
                if (particle is IDynamic)
                    dynamicManager.Add(particle);
            }
        }

        public override void Play()
        {
            dynamicManager.Run(executionTime);
        }
    }
}
