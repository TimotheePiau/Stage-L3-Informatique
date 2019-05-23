using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    class SimpleDynamicSituationFactory : ISituationFactory
    {
        public Structure2D structure { get; private set; }
        public IParticleFactory particlesFactory { get; private set; }
        public List<IDecidable> decisionMakers { get; private set; }
        public float executionTime { get; private set; }

        public SimpleDynamicSituationFactory(Structure2D structure, IParticleFactory particlesFactory, List<IDecidable> decisionMakers, float executionTime)
        {
            this.structure = structure;
            this.particlesFactory = particlesFactory;
            this.decisionMakers = decisionMakers;
            this.executionTime = executionTime;
        }

        public DynamicSituation CreateSituation()
        {
            DynamicManager2D dynamicManager = new DynamicManager2D();
            
            List<IDecidable> duplicateDecisionMakers =  new List<IDecidable>();
            foreach(IDecidable decisionMaker in decisionMakers)
            {
                duplicateDecisionMakers.Add(decisionMaker.Duplicate());
            }

            List<Particle2D> particles = particlesFactory.CreateParticles(structure, duplicateDecisionMakers);

            return new SimpleDynamicSituation(dynamicManager, structure.Duplicate(), particles, duplicateDecisionMakers, executionTime);
        }
    }
}
