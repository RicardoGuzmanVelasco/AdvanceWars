using System;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public partial class Battalion
    {
        public Unit Unit { get; init; } = Unit.Null;
        public Nation AllegianceTo { get; init; }
        public int Forces { get; set; } = 100;

        public int Platoons => Math.Max(1, Forces / 10);

        public MovementRate MovementRate => Unit.Mobility;
        public Propulsion Propulsion => Unit.Propulsion;

        public bool IsEnemy([NotNull] Battalion other) => !IsFriend(other);

        public bool IsFriend([NotNull] Battalion other)
        {
            return !other.AllegianceTo.IsStateless &&
                   !AllegianceTo.IsStateless &&
                   AllegianceTo.Equals(other.AllegianceTo);
        }

        public int BaseDamageTo(Armor other)
        {
            return Unit.BaseDamageTo(other);
        }

        public Battalion Clone()
        {
            return MemberwiseClone() as Battalion;
        }

        #region Formatting
        public override string ToString()
        {
            return $"{nameof(Unit)}: {Unit}, {nameof(AllegianceTo)}: {AllegianceTo}, {nameof(Forces)}: {Forces}";
        }
        #endregion
    }
}