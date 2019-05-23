using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    class SimpleDynamicSituation : DynamicSituation
    {
        public SimpleDynamicSituation(DynamicManager2D dynamicManager,Structure2D structure, List<Particle2D> particles, List<IDecidable> decisionMakers, float executionTime)
        {
            this.dynamicManager = dynamicManager;
            this.structure = structure;
            this.particles = particles;
            this.decisionMakers = decisionMakers;
            this.executionTime = executionTime;

            setup();
        }
        
        private void setup()
        {
            if (structure is IDynamic)
                dynamicManager.Add((IDynamic)structure);

            foreach(IDecidable decisionMaker in decisionMakers)
            {
                if (decisionMaker is IDynamic)
                    dynamicManager.Add((IDynamic)decisionMaker);
            }

            foreach(Particle2D particle in particles)
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
