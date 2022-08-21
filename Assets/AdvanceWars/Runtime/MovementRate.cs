using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public struct MovementRate
    {
        readonly int value;

        MovementRate(int value)
        {
            Require(value).Not.Negative();
            this.value = value;
        }

        public static implicit operator MovementRate(int rate)
        {
            return new MovementRate(rate);
        }

        public static implicit operator int(MovementRate rate)
        {
            return rate.value;
        }

        public static MovementRate None => 0;

        public override string ToString() => value.ToString();
    }
}