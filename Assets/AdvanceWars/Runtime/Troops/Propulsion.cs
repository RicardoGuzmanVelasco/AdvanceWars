namespace AdvanceWars.Runtime
{
    public readonly struct Propulsion
    {
        public Propulsion(string propulsionId)
        {
            Id = propulsionId;
        }

        public static Propulsion None => new Propulsion("");
        public static Propulsion Air => new Propulsion("Air");

        string Id { get; }

        public override string ToString()
        {
            return this.Equals(None) ? "None" : $"{Id}";
        }
    }
}