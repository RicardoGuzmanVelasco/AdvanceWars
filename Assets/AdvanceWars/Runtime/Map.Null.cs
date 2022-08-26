namespace AdvanceWars.Runtime
{
    public partial record Map
    {
        public static Map Null { get; } = new NoMap();

        internal record NoMap : Map
        {
            public NoMap() : base(0, 0) { }

            public override Space WhereIs(Allegiance battalion)
            {
                return Space.Null;
            }
        }
    }
}