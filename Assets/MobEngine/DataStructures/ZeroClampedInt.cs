using System;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Extensions.DataStructures
{
    public class ZeroClampedInt
    {
        const int Floor = 0;
        int value;

        public ZeroClampedInt(int value, int ceil)
        {
            Require(value).Between(Floor, ceil);
            
            Value = value;
            Ceil = ceil;
        }

        public int Value
        {
            get => Math.Clamp(value, Floor, Ceil);
            set => this.value = value;
        }

        public int Ceil { get; }

        public static implicit operator int(ZeroClampedInt ci) => ci.Value;
        public static implicit operator ZeroClampedInt(int ci) => new(ci, ci);
        public static ZeroClampedInt operator +(ZeroClampedInt a, int b)
        {
            return new(value: Math.Clamp(a.Value + b, Floor, a.Ceil), ceil: a.Ceil);
        }
    }
}