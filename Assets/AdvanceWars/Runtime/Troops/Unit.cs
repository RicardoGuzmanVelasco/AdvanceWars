using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public record Unit
    {
        public MovementRate Mobility { get; init; } = MovementRate.None;
        public Propulsion Propulsion { get; init; } = Propulsion.None;
        public Weapon Weapon { get; init; } = Weapon.Null;

        public Armor Armor { get; init; } = new();
        public RangeOfFire RangeOfFire { get; init; } = RangeOfFire.One;

        public int BaseDamageTo([NotNull] Armor other)
        {
            Require(other).Not.Null();
            return Weapon.BaseDamageTo(other);
        }

        public static Unit Null { get; } = new();

        public bool IsAerial()
        {
            return Propulsion.Equals(Propulsion.Air);
        }
    }
}