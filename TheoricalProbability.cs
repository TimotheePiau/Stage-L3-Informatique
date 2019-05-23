using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    class TheoricalProbability : StatTool
    {
        public double[,] HeatMap { get; private set; }
        public int IterationCount { get; private set; }
        public double totalProbability { get; private set; }

        private ResultPrinter resultPrinter;

        public TheoricalProbability(DynamicSituation dynamicSituation,string filename)
        {
            currentSituation = dynamicSituation;
            resultPrinter = new ResultPrinter(filename, DataType.HEATMAP);

            totalProbability = 0;
        }

        public override void updateSituation(DynamicSituation newSituation)
        {
            Console.WriteLine("Vous ne pouvez pas update la situation dynamic d'un outils théorique!");
        }

        public override void setup()
        {
            if (currentSituation != null)
            {
                if (HeatMap == null)
                {
                    HeatMap = new double[((ManhattanLattice2D)(currentSituation.structure)).xmax * 2 + 1, ((ManhattanLattice2D)(currentSituation.structure)).xmax * 2 + 1];
                    
                    for (int l = 0; l < HeatMap.GetLength(0); l++)
                    {
                        for (int r = 0; r < HeatMap.GetLength(1); r++)
                        {
                            HeatMap[l, r] = 0;
                        }
                    }
                }

                currentSituation.dynamicManager.AddMonitoring(OnSituationEnd);
            }
        }

        //foreach (ID element in monitoredElement)
        //{
        //    if (element.type == ElementType.Particle)
        //    {
        //        currentSituation.particles[element.number].addMonitoring(OnChangeNotificaction);
        //    }
        //}

        public override void computeResult()
        {
            resultPrinter.PrintByteResult(HeatMap);
        }

        //public virtual void OnChangeNotification(ID changedElementID)
        //{

        //}
        

        public void OnSituationEnd()
        {
            if (currentSituation != null)
            {
                foreach (Particle2D particle in currentSituation.dynamicManager.particles)
                {
                    HeatMap[ConvertLineIndex(particle.X), ConvertRowIndex(particle.Y)] += ((TheoricalParticle2D)particle).probability;
                    totalProbability += ((TheoricalParticle2D)particle).probability;


                    //Console.WriteLine("({0},{1}) : {2}", particle.X, particle.Y, ((TheoricalParticle2D)particle).probability);
                }
            }
        }

        private int ConvertLineIndex(int x)
        {
            return x + ((HeatMap.GetLength(0) - 1) / 2);
        }

        private int ConvertRowIndex(int y)
        {
            return y + ((HeatMap.GetLength(1) - 1) / 2);
        }
    }
}

