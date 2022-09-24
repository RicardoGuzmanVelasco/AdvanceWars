namespace AdvanceWars.Runtime
{
    public readonly struct Tactic
    {
        readonly string id;

        Tactic(string id) => this.id = id;

        #region Factory methods
        public static Tactic None { get; } = new();

        public static Tactic Wait { get; } = new("Wait");
        public static Tactic Fire { get; } = new("Fire");
        public static Tactic Move { get; } = new("Move");
        public static Tactic Siege { get; } = new("Siege");
        public static Tactic Merge { get; } = new("Merge");
        #endregion

        public override string ToString() => id;
    }
}