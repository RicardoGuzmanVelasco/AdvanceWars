using JetBrains.Annotations;

namespace AdvanceWars.Runtime.Domain.Troops
{
    public readonly struct Propulsion
    {
        readonly string id;

        public Propulsion([NotNull] string propulsionId) => id = propulsionId;

        public static Propulsion None { get; } = new();

        public override string ToString() => id ?? "None";
    }
}