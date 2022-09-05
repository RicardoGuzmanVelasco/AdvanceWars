using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public readonly struct Nation
    {
        public string Id { get; }

        public Nation(string id)
        {
            Require(id).Not.NullOrWhiteSpace();
            Id = id;
        }

        public static Nation Stateless => new Nation();

        public override string ToString()
        {
            return Id;
        }
    }
}