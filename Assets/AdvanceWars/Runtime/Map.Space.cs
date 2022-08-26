using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public partial record Map
    {
        public class Space
        {
            public Terrain Terrain { get; set; } = Terrain.Null;

            public Battalion Occupant { get; private set; } = Battalion.Null;

            public bool IsOccupied => Occupant != Battalion.Null;

            public bool IsBesiegable
            {
                get
                {
                    Require(Terrain is Building).True();
                    return !Terrain.IsAlly(Occupant);
                }
            }

            public void Besiege()
            {
                Require(IsOccupied).True();

                var outcome = Terrain.SiegeOutcome(Occupant);
                Terrain.SiegePoints = outcome.SiegePoints;
            }

            public bool IsHostileTo(Allegiance other)
            {
                return IsOccupied && Occupant.IsEnemy(other);
            }

            public void Occupy(Battalion occupant)
            {
                Occupant = occupant;
            }

            public void Unoccupy()
            {
                Occupant = Battalion.Null;
                Terrain.LiftSiege();
            }

            public bool IsCrossableBy(Allegiance battalion) => !IsHostileTo(battalion);

            public void ReportCasualties(int forcesAfter)
            {
                Require(IsOccupied).True();

                Occupant.Forces = forcesAfter;
                if(Occupant.Forces <= 0)
                    Unoccupy();
            }
        }
    }
}