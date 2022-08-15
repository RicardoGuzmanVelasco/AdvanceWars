namespace AdvanceWars.Runtime
{
    public partial record Map
    {
        public class Space
        {
            public Terrain Terrain { get; set; } //TODO: debería ser init.
            public Unit Occupant { get; set; } = Unit.Null;
            public bool IsOccupied => Occupant is not Unit.NoUnit;

            public bool IsHostileTo(Unit unit)
            {
                return IsOccupied && Occupant.IsEnemy(unit);
            }

            public bool IsCrossableBy(Unit unit) => !IsHostileTo(unit);
        }
    }
}