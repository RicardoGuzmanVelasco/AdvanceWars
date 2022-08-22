namespace AdvanceWars.Runtime
{
    public struct Tactic
    {
        public static Tactic Wait()
        {
            return new Tactic("Wait");
        }

        Tactic(string id)
        {
            Id = id;
        }

        public string Id { get; init; }

        public override string ToString() => Id;
    }
}