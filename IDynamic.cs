using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    public interface IDynamic
    {
        float NextUpdateTime { get; set; }
 
        void Update();
        void InitializeNUT(float initialisationTime);
        void UpdateNUT();
        void addMonitoring(StatTool.ChangeDelegate changeDelegate);
    }
}
