using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    class TheoricalWalker : TheoricalParticle2D
    {
        public override float NextUpdateTime { get { return nextUpdateTime; } set { } }

        private float nextUpdateTime;

        public TheoricalWalker(DynamicManager2D dynamicManager, int elementIDNumber, Structure2D structure, IDecidable decisionMaker, double probability, Position position = new Position(), float dt = 1)
        {
            elementID = new ID { type = ElementType.Particle, number = elementIDNumber };

            this.dynamicManager = dynamicManager;
            this.structure = structure;
            this.decisionMaker = decisionMaker;

            this.probability = probability;

            this.position = position;

            this.dt = dt;
        }
        
        public override void Update()
        {
            List<int> positionMetric = structure.GetM(position);
            List<double> directionProbability = decisionMaker.TheoricalProbability(positionMetric);

            int nextPossibilities = 0;
            int firstDirection = -1;

            for(int directionIndex = 0; directionIndex < positionMetric.Count; directionIndex++)
            {
                if(positionMetric[directionIndex] > 0)
                {
                    nextPossibilities += 1;
                    if(nextPossibilities > 1)
                    {
                        TheoricalParticle2D alternativeParticle = this.Duplicate();
                        dynamicManager.Add((Particle2D)alternativeParticle);
                        alternativeParticle.UpdateProbability(directionProbability[directionIndex]);
                        alternativeParticle.Move(structure.GetDestination(position, directionIndex));
                        alternativeParticle.UpdateNUT();

                        //Console.WriteLine("particle created : ({0},{1}) p = {2} dt = {3}", alternativeParticle.X, alternativeParticle.Y, alternativeParticle.probability, alternativeParticle.dt);
                        
                    }
                    else
                    {
                        firstDirection = directionIndex;
                    }
                }
            }

            if (firstDirection > -1)
            {
                this.UpdateProbability(directionProbability[firstDirection]);
                this.Move(structure.GetDestination(position, firstDirection));
                //this.UpdateNUT();
            }
        }

        public override void Move(Position destination)
        {
            position.x = destination.x;
            position.y = destination.y;
        }

        public override void InitializeNUT(float initialisationTime)
        {
            nextUpdateTime = initialisationTime;
        }

        public override void UpdateNUT()
        {
            nextUpdateTime += dt;
        }

        public override void UpdateProbability(double moveProbability)
        {
            probability *= moveProbability;
        }

        public override TheoricalParticle2D Duplicate()
        {
            return new TheoricalWalker(dynamicManager, dynamicManager.GetNewParticleID(), structure, decisionMaker.Duplicate(), probability, position, dt);
        }
    }
}
