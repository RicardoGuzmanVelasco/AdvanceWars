﻿using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public readonly struct MovementRate
    {
        readonly int value;

        MovementRate(int value)
        {
            Require(value).Not.Negative();
            this.value = value;
        }

        public static MovementRate None => 0;

        public static implicit operator MovementRate(int rate)
        {
            return new MovementRate(rate);
        }

        public static implicit operator int(MovementRate rate)
        {
            return rate.value;
        }

        public override string ToString()
        {
            return this.Equals(None) ? "None" : value.ToString();
        }
    }
}