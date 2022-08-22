namespace AdvanceWars.Runtime
{
    public partial class Battalion
    {
        public static Battalion Null { get; } = new NoBattalion();

        internal class NoBattalion : Battalion
        {
            public NoBattalion()
            {
                Unit = Unit.Null;
                Forces = 0;
            }

            public override string ToString()
            {
                return "NoBattalion";
            }
        }
    }
}