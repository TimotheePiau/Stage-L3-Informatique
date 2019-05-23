using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    public class DynamicManager2D
    {
        public int particleIDCount { get; private set; } = 0;
        public int structureIDCount { get; private set; } = 0;
        public int decisionMakerIDCount { get; private set; } = 0;

        public float Time { get; private set; } = 0;

        public List<IDynamic> particles;
        public List<IDynamic> structures;
        public List<IDynamic> decisionMakers;

        private StatTool.SituationEndDelegate onEnd;

        public DynamicManager2D()
        {
            particles = new List<IDynamic>();
            structures = new List<IDynamic>();
            decisionMakers = new List<IDynamic>();
        }

        public void Add(IDynamic element)
        {
            element.InitializeNUT(Time);

            if (element is Particle2D)
                particles.Add(element);
            else if (element is Structure2D)
                structures.Add(element);
            else if (element is IDecidable)
                decisionMakers.Add(element);
        }

        public void Run(float timeToStop)
        {
            CheckElementsNUT();
            Routine(timeToStop);

            //Console.WriteLine("Position : ({0},{1})", ((Particle2D)particles[0]).X, ((Particle2D)particles[0]).Y);

            if (onEnd != null)
                onEnd();
        }

        public void CheckElementsNUT()
        {
            for(int p = 0; p < particles.Count; p++)
            {
                if (particles[p].NextUpdateTime < Time)
                    particles[p].InitializeNUT(Time);
            }

            for(int s = 0; s < structures.Count; s++)
            {
                if (structures[s].NextUpdateTime < Time)
                    structures[s].InitializeNUT(Time);
            }

            for (int d = 0; d < decisionMakers.Count; d++)
            {
                if (decisionMakers[d].NextUpdateTime < Time)
                    decisionMakers[d].InitializeNUT(Time);
            }
        }

        public void Routine(float timeToStop)
        {
            List<IDynamic> NFElements = FindNextFrameElements();

            if (NFElements.Any())
            {
                while (NFElements[0].NextUpdateTime < timeToStop)
                {
                    UpdateTime(NFElements[0].NextUpdateTime);

                    FrameExecution(NFElements);

                    NFElements = FindNextFrameElements();

                    if (!NFElements.Any())
                    {
                        break;
                    }
                }
            }

            UpdateTime(timeToStop);
        }

        private List<IDynamic> FindNextFrameElements()
        {
            List<IDynamic> NFElements = new List<IDynamic>();
            float minNUT = -1;

            for(int s = 0; s < structures.Count; s++)
            {
                if(minNUT < 0 || structures[s].NextUpdateTime < minNUT)
                {
                    NFElements.RemoveRange(0, NFElements.Count);
                    NFElements.Add(structures[s]);
                    minNUT = structures[s].NextUpdateTime;
                }
                else if (structures[s].NextUpdateTime == minNUT)
                {
                    NFElements.Add(structures[s]);
                }
            }

            for (int d = 0; d < decisionMakers.Count; d++)
            {
                if (minNUT < 0 || decisionMakers[d].NextUpdateTime < minNUT)
                {
                    NFElements.RemoveRange(0, NFElements.Count);
                    NFElements.Add(decisionMakers[d]);
                    minNUT = decisionMakers[d].NextUpdateTime;
                }
                else if (decisionMakers[d].NextUpdateTime == minNUT)
                {
                    NFElements.Add(decisionMakers[d]);
                }
            }

            for (int p = 0; p < particles.Count; p++)
            {
                if (minNUT < 0 || particles[p].NextUpdateTime < minNUT)
                {
                    NFElements.RemoveRange(0, NFElements.Count);
                    NFElements.Add(particles[p]);
                    minNUT = particles[p].NextUpdateTime;
                }
                else if(particles[p].NextUpdateTime == minNUT)
                {
                    NFElements.Add(particles[p]);
                }
            }

            return NFElements;
        }

        private void FrameExecution(List<IDynamic> frameElements)
        {
            for(int e = 0; e < frameElements.Count; e++)
            {
                frameElements[e].Update();
                frameElements[e].UpdateNUT();
            }
        }

        private void UpdateTime(float newTime)
        {
            Time = newTime;
        }

        public void ResetTime()
        {
            Time = 0;
        }

        public void AddMonitoring(StatTool.SituationEndDelegate onSituationEndMethod)
        {
            onEnd += onSituationEndMethod;
        }

        public int GetNewParticleID()
        {
            return particleIDCount++;
        }

        public int GetNewStructureID()
        {
            return structureIDCount++;
        }

        public int GetNewDecisionMakerID()
        {
            return decisionMakerIDCount++;
        }
    }
}
