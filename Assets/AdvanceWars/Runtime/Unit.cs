namespace AdvanceWars.Runtime
{
    public class Unit
    {
        public Nation Motherland { get; init; }
        public MovementRate MovementRate { get; init; } = 3; //TODO: no se puede subir con esto.

        public bool IsEnemy(Unit other) => !IsFriend(other);
        public bool IsFriend(Unit other) => Motherland == other.Motherland;

        public static Unit Null => new NoUnit();

        internal class NoUnit : Unit { }
    }

    public record Nation(string Id);
}