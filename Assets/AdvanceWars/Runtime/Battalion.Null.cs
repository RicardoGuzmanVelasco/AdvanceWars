namespace AdvanceWars.Runtime
{
    public partial class Battalion
    {
        public static Battalion Null { get; } = new NoBattalion();

        class NoBattalion : Battalion, INull
        {
            public NoBattalion()
            {
                Unit = Unit.Null;
                Forces = 0;
            }

            public override string ToString()
            {
                return this.GetType().Name;
            }
        }
    }
}