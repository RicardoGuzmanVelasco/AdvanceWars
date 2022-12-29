using System;

namespace AdvanceWars.Runtime.Extensions.DataStructures
{
    public class CeiledInt
    {
        int value;

        public CeiledInt(int value, int ceil)
        {
            Value = value;
            Ceil = ceil;
        }

        public int Value
        {
            get => Math.Min(value, Ceil);
            set => this.value = value;
        }

        public int Ceil { get; }

        public static implicit operator int(CeiledInt ci) => ci.Value;
        public static implicit operator CeiledInt(int ci) => new(ci, ci);
        public static CeiledInt operator +(CeiledInt a, int b) => new(value: a.Value + b, ceil: a.Ceil);
    }
}