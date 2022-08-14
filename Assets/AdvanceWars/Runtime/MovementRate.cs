using RGV.DesignByContract.Runtime;

namespace AdvanceWars.Runtime
{
    public struct MovementRate
    {
        readonly int value;

        MovementRate(int value)
        {
            Precondition.Require(value).Not.Negative();
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
    }
}