using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public readonly struct RangeOfFire
    {
        public int Min { get; }
        public int Max { get; }

        public RangeOfFire(int min, int max)
        {
            Require(min).Positive();
            Require(max).Positive();
            Require(min).LesserOrEqualThan(max);

            this.Min = min;
            this.Max = max;
        }

        public static RangeOfFire One => new RangeOfFire(1, 1);
        
        public bool IsValid()
        {
            return Min > 0 && Max > 0 && Min <= Max;
        }
    }
}