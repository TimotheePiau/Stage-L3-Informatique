using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    public abstract class Particle2D : IDynamic
    {
        public ID elementID { get; protected set; }

        public int X { get { return position.x; } set { } }
        public int Y { get { return position.y; } set { } }
        public float dt { get; protected set; }
        public abstract float NextUpdateTime { get; set; }
        protected StatTool.ChangeDelegate changeDelegate;

        protected Position position;
        protected Structure2D structure;
        protected IDecidable decisionMaker;

        public abstract void Move(Position destination);

        public abstract void Update();
        public abstract void InitializeNUT(float initialisationTime);
        public abstract void UpdateNUT();

        public void addMonitoring(StatTool.ChangeDelegate changeDelegate)
        {
            this.changeDelegate += changeDelegate; 
        }
    }
}
