using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    class Sequencer
    {
        public ISituationFactory situationBuilder { get; private set; }
        public StatTool resultManager;

        public Sequencer(StatTool resultManager, ISituationFactory situationBuilder)
        {
            this.resultManager = resultManager; //new StatFinalPosition(monitoredElement, filename);
            this.situationBuilder = situationBuilder; //new SimpleDynamicSituationFactory(structure, particlesFactory, decisionMaker, executionTime);
        }

        public void Run(int repetition)
        {
            for(int i = 0; i < repetition; i++)
            {
                DynamicSituation dynamicSituation = situationBuilder.CreateSituation();

                resultManager.updateSituation(dynamicSituation);
                resultManager.setup();

                dynamicSituation.Play();
            }
            Console.WriteLine("Data Calculation complete!");

            resultManager.computeResult();
            Console.WriteLine("Print complete!");
        }
    }
}
