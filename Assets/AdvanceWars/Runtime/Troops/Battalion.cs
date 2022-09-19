using System;

namespace AdvanceWars.Runtime
{
    public partial class Battalion : Allegiance
    {
        public Unit Unit { private get; init; } = Unit.Null;

        public int Forces { get; set; } = 100;

        public int Platoons => Math.Max(1, Forces / 10);

        public MovementRate MovementRate => Unit.Mobility;
        public Propulsion Propulsion => Unit.Propulsion;
        public Armor Armor => Unit.Armor;
        public RangeOfFire RangeOfFire => Unit.RangeOfFire;

        public bool Damaged => Forces != 100;
        public int BaseDamageTo(Armor other)
        {
            return Unit.BaseDamageTo(other);
        }

        public Battalion Clone()
        {
            return MemberwiseClone() as Battalion;
        }
        
        public bool EqualUnit(Battalion other)
        {
            return Unit.Equals(other.Unit);
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