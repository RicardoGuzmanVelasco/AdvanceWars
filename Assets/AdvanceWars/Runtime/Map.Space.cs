using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public partial record Map
    {
        public class Space
        {
            public Terrain Terrain { get; set; } = Terrain.Null;

            public Battalion Occupant { get; private set; } = Battalion.Null;

            public bool IsOccupied => Occupant is not Battalion.NoBattalion;

            public bool ThereIsAnOccupantEnemyToTheTerrainOwner
            {
                get
                {
                    Require(Terrain is Building).True();
                    return (Terrain as Building).IsEnemy(Occupant);
                }
            }

            public bool IsOccupiedByEnemyOf(Allegiance other)
            {
                return IsOccupied && Occupant.IsEnemy(other);
            }

            public void Occupy(Battalion occupant)
            {
                Occupant = occupant;
                //todo cosas.
            }

            public void Unoccupy()
            {
                Occupant = Battalion.Null;

                var building = Terrain as Building;
                building?.LiftSiege();
            }

            public bool IsCrossableBy(Allegiance battalion) => !IsOccupiedByEnemyOf(battalion);

            public void ReportCasualties(int forcesAfter)
            {
                Require(IsOccupied).True();

                Occupant.Forces = forcesAfter;
                if(Occupant.Forces <= 0)
                    Unoccupy();
            }

            public void Besiege()
            {
                Require(IsOccupied).True();

                var building = (Terrain as Building);
                var outcome = building.SiegeOutcome(Occupant);
                building.SiegePoints = outcome.SiegePoints;
                // TODO: Require(IsOccupiedByEnemyOf(Occupant)).True();
            }
        }
    }
}