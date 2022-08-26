namespace AdvanceWars.Runtime
{
    public readonly struct Tactic
    {
        string Id { get; }

        Tactic(string id)
        {
            Id = id;
        }

        #region Factory methods
        public static Tactic Wait => new Tactic("Wait");
        public static Tactic Fire => new Tactic("Fire");
        public static Tactic Move => new Tactic("Move");
        public static Tactic Siege => new Tactic("Siege");
        #endregion

        public override string ToString() => Id;
    }
}