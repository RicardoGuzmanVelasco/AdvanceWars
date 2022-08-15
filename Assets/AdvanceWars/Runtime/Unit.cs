namespace AdvanceWars.Runtime
{
    public class Unit
    {
        public Nation AllegianceTo { get; init; }
        public MovementRate MovementRate { get; init; }
        public Propulsion Propulsion { get; init; }

        public bool IsEnemy(Unit other) => !IsFriend(other);
        public bool IsFriend(Unit other) => AllegianceTo == other.AllegianceTo;

        public override string ToString()
        {
            return @$"{nameof(AllegianceTo)}: {AllegianceTo},
                    {nameof(MovementRate)}: {MovementRate},
                    {nameof(Propulsion)}: {Propulsion}";
        }

        public static Unit Null => new NoUnit();

        internal class NoUnit : Unit { }
    }

    public record Nation(string Id);
}