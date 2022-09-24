using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Troops
{
    public record Armor
    {
        public string Id { get; }

        public Armor() : this("") { }

        public Armor([NotNull] string id)
        {
            Require(id).Not.Null();
            Id = id;
        }

        public override string ToString() => $"{Id}";
    }
}