﻿using System;

namespace AdvanceWars.Runtime
{
    public class Batallion
    {
        public Unit Unit { get; init; }
        public Nation AllegianceTo { get; init; }
        public MovementRate MovementRate => Unit.Mobility;
        public Propulsion Propulsion => Unit.Propulsion;
        public int Forces { get; set; }
        public int Platoons => Math.Max(1, Forces / 10);
        public bool IsEnemy(Batallion other) => !IsFriend(other);
        public bool IsFriend(Batallion other) => AllegianceTo == other.AllegianceTo;

        public override string ToString()
        {
            return @$"{nameof(AllegianceTo)}: {AllegianceTo},
                    {nameof(Unit.Mobility)}: {Unit.Mobility},
                    {nameof(Unit.Propulsion)}: {Unit.Propulsion}";
        }

        public static Batallion Null => new NoBatallion
        {
            Unit = new Unit
            {
                Mobility = MovementRate.None,
                Propulsion = Propulsion.None
            }
        };

        internal class NoBatallion : Batallion { }

        public int BaseDamageTo(Unit other)
        {
            return Unit.Weapon.BaseDamageTo(other);
        }
    }

    public record Nation(string Id);
}