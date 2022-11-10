using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Troops
{
    public partial record Unit
    {
        public MovementRate Mobility { get; init; } = MovementRate.None;
        public Propulsion Propulsion { get; init; } = Propulsion.None;
        public Weapon Weapon { get; init; } = Weapon.Null;
        public Military ServiceBranch { get; init; } = Military.None;
        public Armor Armor { get; init; } = new();
        public RangeOfFire RangeOfFire { get; init; } = RangeOfFire.One;
        public Price Price { get; init; } = 0;

        public int BaseDamageTo([NotNull] Armor other)
        {
            Require(other).Not.Null();
            return Weapon.BaseDamageTo(other);
        }

        public static Unit Null { get; } = new();

        public bool IsAerial()
        {
            return ServiceBranch.Equals(Military.AirForce);
        }
    }
}