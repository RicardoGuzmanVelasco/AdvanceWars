namespace AdvanceWars.Runtime
{
    public partial record Map
    {
        public class Space
        {
            public Terrain Terrain { get; set; } = Terrain.Null; //TODO: debería ser init.
            public Battalion Occupant { get; set; } = Battalion.Null;
            public bool IsOccupied => Occupant is not Battalion.NoBattalion;

            public bool IsHostileTo(Battalion battalion)
            {
                return IsOccupied && Occupant.IsEnemy(battalion);
            }

            public bool IsCrossableBy(Battalion battalion) => !IsHostileTo(battalion);
        }
    }
}