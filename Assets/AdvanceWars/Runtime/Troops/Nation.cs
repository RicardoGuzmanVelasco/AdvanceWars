using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public readonly struct Nation
    {
        readonly string id;

        public Nation(string id)
        {
            Require(id).Not.NullOrWhiteSpace();
            this.id = id;
        }

        public static Nation Stateless { get; } = new();

        public override string ToString() => id;
    }
}