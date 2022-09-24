using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Troops
{
    public readonly struct RangeOfFire
    {
        public int Min { get; }
        public int Max { get; }

        public RangeOfFire(int min, int max)
        {
            Require(min).Positive();
            Require(max).GreaterOrEqualThan(min);

            this.Min = min;
            this.Max = max;
        }

        public static RangeOfFire Zero { get; } = new();
        public static RangeOfFire One { get; } = new(1, 1);

        public override string ToString() => $"({Min}, {Max})";
    }
}