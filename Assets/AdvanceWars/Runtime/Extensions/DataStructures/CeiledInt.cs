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
    }
}