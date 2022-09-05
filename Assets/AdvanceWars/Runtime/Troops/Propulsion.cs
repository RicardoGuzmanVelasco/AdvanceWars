namespace AdvanceWars.Runtime
{
    public readonly struct Propulsion
    {
        public Propulsion(string propulsionId)
        {
            Id = propulsionId;
        }

        public static Propulsion None => new Propulsion("");

        string Id { get; }

        public override string ToString()
        {
            return this.Equals(None) ? "None" : $"{Id}";
        }
    }
}