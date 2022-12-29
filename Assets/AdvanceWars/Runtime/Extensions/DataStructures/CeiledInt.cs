using System;

namespace AdvanceWars.Runtime.Extensions.DataStructures
{
    public class CeiledInt
    {
        int value;
        readonly int ceil;

        public CeiledInt(int value, int ceil)
        {
            Value = value;
            this.ceil = ceil;
        }

        public int Value
        {
            get => Math.Min(value, ceil);
            set => this.value = value;
        }

        public static implicit operator int(CeiledInt ci) => ci.Value;
        public static implicit operator CeiledInt(int ci) => new CeiledInt(ci, ci);
        public static CeiledInt operator +(CeiledInt a, int b) => new CeiledInt(value: a.Value + b, ceil: a.ceil);
    }
}