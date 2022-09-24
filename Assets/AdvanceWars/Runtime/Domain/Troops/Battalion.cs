using System;

namespace AdvanceWars.Runtime
{
    public partial class Battalion : Allegiance
    {
        public const int MaxForces = 100;
        private const int PlatoonSize = 10;

        public Unit Unit { private get; init; } = Unit.Null;

        public int Forces { get; set; } = MaxForces;

        public int Platoons => Math.Max(1, Forces / PlatoonSize);

        public MovementRate MovementRate => Unit.Mobility;
        public Propulsion Propulsion => Unit.Propulsion;
        public Armor Armor => Unit.Armor;
        public RangeOfFire RangeOfFire => Unit.RangeOfFire;
        public bool Damaged => Forces < MaxForces;

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

        #region Formatting
        public override string ToString()
        {
            return $"{nameof(Unit)}: {Unit}, {nameof(Motherland)}: {Motherland}, {nameof(Forces)}: {Forces}";
        }
        #endregion
    }
}