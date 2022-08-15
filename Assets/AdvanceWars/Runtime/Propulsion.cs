namespace AdvanceWars.Runtime
{
    public readonly struct Propulsion
    {
        public Propulsion(string propulsionId)
        {
            Id = propulsionId;
        }

        public string Id { get; }
        public static Propulsion None => new Propulsion("");

        public override string ToString() => $"{Id}";
    }
}