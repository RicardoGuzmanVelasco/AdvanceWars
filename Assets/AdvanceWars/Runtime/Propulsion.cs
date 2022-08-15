namespace AdvanceWars.Runtime
{
    public readonly struct Propulsion
    {
        public Propulsion(string propulsionId)
        {
            Id = propulsionId;
        }

        public string Id { get; }

        public override string ToString() => $"{Id}";
    }
}