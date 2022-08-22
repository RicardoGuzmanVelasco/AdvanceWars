using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public partial record Map
    {
        public class Space
        {
            public Terrain Terrain { get; set; } = Terrain.Null;
            public Battalion Occupant { get; set; } = Battalion.Null;
            public bool IsOccupied => Occupant is not Battalion.NoBattalion;

            public bool IsHostileTo(Battalion battalion)
            {
                return IsOccupied && Occupant.IsEnemy(battalion);
            }

            public bool IsCrossableBy(Battalion battalion) => !IsHostileTo(battalion);

            public void ReportCasualties(int forcesAfter)
            {
                Require(IsOccupied).True();

                Occupant.Forces = forcesAfter;
                if(Occupant.Forces <= 0)
                    Occupant = Battalion.Null;
            }
        }
    }
}