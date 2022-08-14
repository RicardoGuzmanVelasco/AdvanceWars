namespace AdvanceWars.Runtime
{
    public partial record Map
    {
        public class Space
        {
            public Unit Occupant { get; set; } = Unit.Null;
            public bool HasUnit => Occupant is not Unit.NoUnit;

            public bool IsHostileTo(Unit unit)
            {
                return HasUnit && Occupant.IsEnemy(unit);
            }
        }
    }
}