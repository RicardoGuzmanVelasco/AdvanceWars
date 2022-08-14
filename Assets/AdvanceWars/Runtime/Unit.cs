namespace AdvanceWars.Runtime
{
    public class Unit
    {
        public Nation Motherland { get; init; }

        public bool IsFriendly(Unit other)
        {
            return Motherland == other.Motherland;
        }
    }

    public record Nation(string Id);
}