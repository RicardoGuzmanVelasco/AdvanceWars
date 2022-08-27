namespace AdvanceWars.Runtime
{
    public partial class Building
    {
        internal static Building Unbesiegable { get; } = new UnbesiegableSpecialCase();

        public class UnbesiegableSpecialCase : Building
        {
            public UnbesiegableSpecialCase() : base(int.MaxValue) { }
        }
    }
}