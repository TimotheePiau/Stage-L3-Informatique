using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk 
{
    

    class Walker2D : Particle2D
    {
        public override float NextUpdateTime { get { return nextUpdateTime; } set { } }

        private float nextUpdateTime;

        public Walker2D(int elementIDNumber, Structure2D structure, IDecidable decisionMaker, Position position = new Position(), float dt = 1)
        {
            elementID = new ID { type = ElementType.Particle, number = elementIDNumber };
            this.structure = structure;
            this.decisionMaker = decisionMaker;

            this.position = position;

            this.dt = dt;
        }

        public override void Update()
        {
            Move(structure.GetDestination(position, decisionMaker.Decide(structure.GetM(position))));
        }

        public override void InitializeNUT(float initialisationTime)
        {
            nextUpdateTime = initialisationTime;
        }

        public override void Move(Position destination)
        {
            position.x = destination.x;
            position.y = destination.y;
        }
        
        public override void UpdateNUT()
        {
            nextUpdateTime += dt;
        }

        public Particle2D generate(int elementIDNumber, Structure2D structure, IDecidable decisionMaker, Position position = new Position(), float dt = 1)
        {
            return new Walker2D(elementIDNumber, structure, decisionMaker, position, dt);
        }
    }
}
