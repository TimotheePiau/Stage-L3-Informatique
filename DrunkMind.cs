using System;
using System.Collections.Generic;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    class DrunkMind : IDecidable
    {
        private Random random;
        
        public DrunkMind(Random random=null)
        {
            if (random == null)
                this.random = new Random();
            else
                this.random = random;
        }

        public int Decide(List<int> metric)
        {
            int directionNumber = -1;

            int totalWeight = 0;
            foreach(int weight in metric)
            {
                totalWeight += weight;
            }

            int randomNumber = random.Next(totalWeight);
            int floorStack = 0;
            
            for(int i = 0; i < metric.Count; i++)
            {
                floorStack += metric[i];

                if(floorStack > randomNumber)
                {
                    directionNumber = i;
                    break;
                }
            }
            
            return directionNumber;
        }

        public List<Double> TheoricalProbability(List<int> metric)
        {
            List<Double> directionProbability = new List<Double>();
            double totalWeight = 0;

            foreach (int directionWeight in metric)
            {
                totalWeight += directionWeight;
            }

            if(totalWeight > 0)
            {
                foreach(int directionWeight in metric)
                {
                    directionProbability.Add(directionWeight / totalWeight);
                }
            }

            return directionProbability;
        }

        public IDecidable Duplicate()
        {
            return new DrunkMind(random);
        }
    }
}
