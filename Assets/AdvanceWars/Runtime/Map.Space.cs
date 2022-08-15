namespace AdvanceWars.Runtime
{
    public partial record Map
    {
        public class Space
        {
            public Terrain Terrain { get; set; } = Terrain.Null; //TODO: debería ser init.
            public Batallion Occupant { get; set; } = Batallion.Null;
            public bool IsOccupied => Occupant is not Batallion.NoBatallion;

            public bool IsHostileTo(Batallion batallion)
            {
                return IsOccupied && Occupant.IsEnemy(batallion);
            }

            public bool IsCrossableBy(Batallion batallion) => !IsHostileTo(batallion);
        }
    }
}