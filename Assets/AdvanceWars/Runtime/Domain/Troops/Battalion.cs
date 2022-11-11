using System;
using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Troops
{
    public partial class Battalion : Allegiance
    {
        public const int MaxForces = 100;
        private const int PlatoonSize = 10;
        public const int MaxPlatoons = MaxForces / PlatoonSize;

        public Unit Unit { private get; init; } = Unit.Null;

        private int forces = MaxForces;
        public int Forces
        {
            get => forces;
            set
            {
                Require(value).GreaterOrEqualThan(0);
                Require(value).LesserOrEqualThan(MaxForces);
                forces = value;
            }
        }

        public int Platoons => Math.Max(1, Forces / PlatoonSize);

        public MovementRate MovementRate => Unit.Mobility;
        public Propulsion Propulsion => Unit.Propulsion;
        public Armor Armor => Unit.Armor;
        public RangeOfFire RangeOfFire => Unit.RangeOfFire;
        public bool Damaged => Forces < MaxForces;
        public int AmmoRounds { get; set; }
        public Price Price => Unit.Price;
        public Price PricePerSoldier => Unit.Price / MaxForces;
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
            
            Forces = Math.Clamp(Forces + reinforces, 0, Battalion.MaxForces);
        }
        
        #region Formatting
        public override string ToString()
        {
            return $"{nameof(Unit)}: {Unit}, {nameof(Motherland)}: {Motherland}, {nameof(Forces)}: {Forces}";
        }
        #endregion
    }
}