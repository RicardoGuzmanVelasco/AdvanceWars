using System;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public class Batallion
    {
        public Unit Unit { get; init; }
        public Nation AllegianceTo { get; init; }
        public int Forces { get; set; }

        public int Platoons => Math.Max(1, Forces / 10);

        public MovementRate MovementRate => Unit.Mobility;
        public Propulsion Propulsion => Unit.Propulsion;

        public bool IsEnemy([NotNull] Batallion other) => !IsFriend(other);

        public bool IsFriend([NotNull] Batallion other)
        {
            return !other.AllegianceTo.IsStateless &&
                   !AllegianceTo.IsStateless &&
                   AllegianceTo.Equals(other.AllegianceTo);
        }

        public int BaseDamageTo(Unit other)
        {
            return Unit.BaseDamageTo(other);
        }

        #region Formatting
        public override string ToString()
        {
            return $"{nameof(Unit)}: {Unit}, {nameof(AllegianceTo)}: {AllegianceTo}, {nameof(Forces)}: {Forces}";
        }
        #endregion

        #region NullObjectPattern
        public static Batallion Null => new NoBatallion
        {
            Unit = new Unit
            {
                Mobility = MovementRate.None,
                Propulsion = Propulsion.None
            }
        };

        internal class NoBatallion : Batallion { }
        #endregion

        public Batallion Clone()
        {
            return MemberwiseClone() as Batallion;
        }
    }
}