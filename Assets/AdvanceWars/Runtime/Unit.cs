namespace AdvanceWars.Runtime
{
    public record Unit
    {
        public MovementRate Mobility { get; init; }
        public Propulsion Propulsion { get; init; }
        public Weapon Weapon { get; init; }
    }
}