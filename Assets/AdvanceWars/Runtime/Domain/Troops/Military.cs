using JetBrains.Annotations;

namespace AdvanceWars.Runtime.Domain.Troops
{
    public readonly struct Military
    {
        readonly string id;

        Military([NotNull] string id) => this.id = id;

        public static Military None { get; } = new();
        public static Military Army { get; } = new("Army");
        public static Military Navy { get; } = new("Navy");
        public static Military AirForce { get; } = new("AirForce");

        public override string ToString() => id;
    }
}