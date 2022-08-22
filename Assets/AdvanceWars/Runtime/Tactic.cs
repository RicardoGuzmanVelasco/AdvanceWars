namespace AdvanceWars.Runtime
{
    public struct Tactic
    {
        public Tactic(string id)
        {
            Id = id;
        }

        public string Id { get; init; }

        public override string ToString() => Id;
    }
}