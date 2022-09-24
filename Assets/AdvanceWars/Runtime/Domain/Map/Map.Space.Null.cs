namespace AdvanceWars.Runtime.Domain.Map
{
    public partial record Map
    {
        public partial class Space
        {
            public static Space Null { get; } = new NoSpace();

            internal class NoSpace : Space
            {
                public override bool IsBesiegable => false;
            }
        }
    }
}