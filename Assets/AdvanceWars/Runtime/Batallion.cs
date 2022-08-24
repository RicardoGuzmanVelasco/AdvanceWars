using System;

namespace AdvanceWars.Runtime
{
    public partial class Battalion : IAllegiance
    {
        public Unit Unit { get; init; } = Unit.Null;
        public Nation Motherland { get; init; }

        public int Forces { get; set; } = 100;

        public int Platoons => Math.Max(1, Forces / 10);

        public MovementRate MovementRate => Unit.Mobility;
        public Propulsion Propulsion => Unit.Propulsion;

        public bool IsEnemy(IAllegiance other) => !IsFriend(other);

        public bool IsFriend(IAllegiance other)
        {
            return !other.Motherland.IsStateless &&
                   !Motherland.IsStateless &&
                   Motherland.Equals(other.Motherland);
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
            return $"{nameof(Unit)}: {Unit}, {nameof(Motherland)}: {Motherland}, {nameof(Forces)}: {Forces}";
        }
        #endregion
    }
}