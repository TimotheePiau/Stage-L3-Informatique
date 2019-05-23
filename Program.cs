using System;
using System.Collections.Generic;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    public struct Position { public int x { get; set; } public int y { get; set; } }

    class Program
    {
        public static void Main(String[] args)
        {
            ///Experimental

            //Random random = new Random();
            ////RandomManhattanLattice2D manhattanLattice = new RandomManhattanLattice2D(1, 200, 200, random);
            //RegularManhattanLattice2D manhattanLattice = new RegularManhattanLattice2D(1, 20, 20, 0, 0);
            //DrunkMind decisionMaker = new DrunkMind(random);
            //Position initialPosition = new Position() { x = 0, y = 0 };

            //List<IDecidable> decisionMakers = new List<IDecidable>();
            //List<Position> particlesInitialPositions = new List<Position>();
            //List<float> dts = new List<float>();

            //decisionMakers.Add(decisionMaker);
            //particlesInitialPositions.Add(initialPosition);
            //dts.Add(1);

            //IParticleFactory particlesFactory = new WalkerFactory(particlesInitialPositions, dts);
            //SimpleDynamicSituationFactory sdsFactory = new SimpleDynamicSituationFactory(manhattanLattice, particlesFactory, decisionMakers, 20);

            //List<ID> monitoredElement = new List<ID>();
            //monitoredElement.Add(new ID { type = ElementType.Particle, number = 0 });
            //StatFinalPosition statModul = new StatFinalPosition(monitoredElement, "RegularML");

            //Sequencer experiment = new Sequencer(statModul, sdsFactory);

            //experiment.Run(100000);

            /// Theorie

            Random random = new Random();
            DynamicManager2D dynamicManager = new DynamicManager2D();
            RegularManhattanLattice2D manhattanLattice = new RegularManhattanLattice2D(dynamicManager.GetNewStructureID(), 20, 20, 0, 0);
            DrunkMind decisionMaker = new DrunkMind(random);
            Position position = new Position() { x = 0, y = 0 };
            TheoricalParticle2D theoricalParticle = new TheoricalWalker(dynamicManager, dynamicManager.GetNewParticleID(), manhattanLattice, decisionMaker, 1, position, 1);

            List<IDecidable> decisionMakers = new List<IDecidable>();
            List<Particle2D> particles = new List<Particle2D>();

            decisionMakers.Add(decisionMaker);
            particles.Add(theoricalParticle);

            TheoricalSituation theoricalSituation = new TheoricalSituation(dynamicManager, manhattanLattice, decisionMakers, particles, 20);

            StatTool theorieManager = new TheoricalProbability(theoricalSituation, "TheoricalRML");

            theorieManager.setup();
            theoricalSituation.Play();
            theorieManager.computeResult();
        }
    }
}
