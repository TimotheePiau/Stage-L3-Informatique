using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    public enum ElementType { Struct, Particle, DMaker };
    public struct ID { public ElementType type; public int number; }

    public abstract class StatTool
    {
        public delegate void ChangeDelegate(ID changedElementID);
        public delegate void SituationEndDelegate();

        public DynamicSituation currentSituation { get; protected set; }
        public List<ID> monitoredElement { get; protected set; }
        
        public abstract void setup();
        public abstract void computeResult();
        public abstract void updateSituation(DynamicSituation newSituation);
    }
}
