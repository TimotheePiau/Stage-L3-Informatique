using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    class StatFinalPosition : StatTool
    {
        public double[,] HeatMap { get; private set; }
        public int IterationCount { get; private set; }
        private ResultPrinter resultPrinter;

        public StatFinalPosition(List<ID> monitoredElement, string filename)
        {
            this.monitoredElement = monitoredElement;
            resultPrinter = new ResultPrinter(filename, DataType.HEATMAP);
        }

        public override void updateSituation(DynamicSituation newSituation)
        {
            currentSituation = newSituation;
        }

        public override void setup()
        {
            if(currentSituation!=null)
            {
                if (HeatMap == null)
                {
                    HeatMap = new double[((ManhattanLattice2D)(currentSituation.structure)).xmax * 2 + 1, ((ManhattanLattice2D)(currentSituation.structure)).xmax * 2 + 1];

                    for(int l = 0; l < HeatMap.GetLength(0); l++)
                    {
                        for(int r = 0; r < HeatMap.GetLength(1); r++)
                        {
                            HeatMap[l,r] = 0; 
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
            if(IterationCount > 0)
                NormalizeResult();
            resultPrinter.PrintByteResult(HeatMap);

            //DisplayHeatMap();
        }

        //public virtual void OnChangeNotification(ID changedElementID)
        //{

        //}

        public void OnSituationEnd()
        {
            if(currentSituation != null)
            {
                foreach (ID element in monitoredElement)
                {
                    if (element.type == ElementType.Particle)
                    {
                        updatePositionProbability(currentSituation.particles[element.number].X, currentSituation.particles[element.number].Y);
                    } 
                }
            }
        }

        private void updatePositionProbability(int x, int y)
        {
            if (x < ((HeatMap.GetLength(0) - 1) / 2) && y < ((HeatMap.GetLength(1) - 1) / 2))
            {
                HeatMap[x + ((HeatMap.GetLength(0) - 1) / 2), y + ((HeatMap.GetLength(1) - 1) / 2)]++;
                IterationCount++;
            }
                
        }

        private void NormalizeResult()
        {
            if(HeatMap != null)
            {
                for(int l = 0; l < HeatMap.GetLength(0); l++)
                {
                    for(int r = 0; r < HeatMap.GetLength(1); r++)
                    {
                        HeatMap[l, r] = HeatMap[l, r] / IterationCount; 
                    }
                }
            }
        }

        private void DisplayHeatMap()
        {
            if (HeatMap != null)
            {
                Console.WriteLine("{0}x{1}", HeatMap.GetLength(0), HeatMap.GetLength(1));
                Console.WriteLine("");
                for (int l = 0; l < HeatMap.GetLength(0); l++)
                {
                    for (int r = HeatMap.GetLength(1) - 1; r >= 0; r--)
                    {
                        Console.Write("(" + HeatMap[l, r].ToString() + ") ");
                    }
                    Console.WriteLine("");
                }
            }
        }
    }
}
