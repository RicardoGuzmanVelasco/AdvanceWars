using RGV.DesignByContract.Runtime;

namespace AdvanceWars.Runtime.Domain.Troops
{
    public readonly struct Price
    {
        readonly int cost;

        Price(int cost)
        {
            Contract.Require(cost).Not.Negative();
            this.cost = cost;
        }

        public static Price Free { get; } = new();

        public static implicit operator Price(int rate)
        {
            return new Price(rate);
        }

        public static implicit operator int(Price rate)
        {
            return rate.cost;
        }

        public override string ToString()
        {
            return this.Equals(Free) ? "None" : cost.ToString();
        }
    }
}