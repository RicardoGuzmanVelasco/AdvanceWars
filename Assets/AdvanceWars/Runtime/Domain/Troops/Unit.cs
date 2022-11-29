using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Troops
{
    public record Unit
    {
        public string Id { get; init; } = "";
        public MovementRate Mobility { get; init; } = MovementRate.None;
        public Propulsion Propulsion { get; init; } = Propulsion.None;
        public Weapon PrimaryWeapon { get; init; } = Weapon.Null;
        public Weapon SecondaryWeapon { get; init; } = Weapon.Null;
        public Military ServiceBranch { get; init; } = Military.None;
        public Armor Armor { get; init; } = new();
        public RangeOfFire RangeOfFire { get; init; } = RangeOfFire.One;
        public Price Price { get; init; } = 0;

        public Weapon WeaponAgainst([NotNull] Armor target)
        {
            Require(target).Not.Null();

            if(PrimaryWeapon.BaseDamageTo(target) > 0)
                return PrimaryWeapon;

            return SecondaryWeapon.BaseDamageTo(target) > 0 ? SecondaryWeapon : Weapon.Null;
        }

        public int BaseDamageTo([NotNull] Armor target)
        {
            Require(target).Not.Null();
            return WeaponAgainst(target).BaseDamageTo(target);
        }

        public static Unit Null { get; } = new();

        public bool IsAerial()
        {
            return ServiceBranch.Equals(Military.AirForce);
        }
    }
}