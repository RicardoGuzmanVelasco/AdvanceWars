namespace AdvanceWars.Runtime
{
    public readonly struct Armor
    {
        public Armor(string id)
        {
            Id = id;
        }

        public string Id { get; }
        public static Armor None => new Armor("");

        public override string ToString() => $"{Id}";
    }
}