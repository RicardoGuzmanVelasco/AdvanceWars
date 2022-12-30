using System;
using AdvanceWars.Runtime.Extensions.DataStructures;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Troops
{
    public partial class Battalion : Allegiance
    {
        public const int MaxForces = 100;
        const int PlatoonSize = 10;

        public Unit Unit { private get; init; } = Unit.Null;

        ZeroClampedInt forces = new(MaxForces, MaxForces);

        public ZeroClampedInt Forces
        {
            get => forces;
            set
            {
                Require(value.Value).GreaterOrEqualThan(0);
                forces = new ZeroClampedInt(value, MaxForces);
            }
        }

        public int Platoons => Math.Max(1, Forces / PlatoonSize);

        public MovementRate MovementRate => Unit.Mobility;
        public Propulsion Propulsion => Unit.Propulsion;
        public Armor Armor => Unit.Armor;
        public RangeOfFire RangeOfFire => Unit.RangeOfFire;
        bool Damaged => Forces < MaxForces;
        public int AmmoRounds { get; set; }
        public Price Price => Unit.Price;
        public Military ServiceBranch => Unit.ServiceBranch;
        public Price PricePerSoldier => Unit.Price / MaxForces;
        public string UnitId => Unit.Id;

        public int BaseDamageTo(Armor other)
        {
            return Unit.BaseDamageTo(other);
        }

        public Battalion Clone()
        {
            return MemberwiseClone() as Battalion;
        }

        public bool CanMergeInto(Battalion target)
        {
            return target.Damaged && Unit.Equals(target.Unit);
        }

        public bool IsAerial()
        {
            return Unit.IsAerial();
        }

        public void Heal(int reinforces)
        {
            Require(reinforces).Positive();

            Forces += reinforces;
        }

        #region Formatting
        public override string ToString()
        {
            return $"{nameof(Unit)}: {Unit}, {nameof(Motherland)}: {Motherland}, {nameof(Forces)}: {Forces}";
        }
        #endregion
    }
}