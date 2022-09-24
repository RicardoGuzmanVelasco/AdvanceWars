using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Runtime.Domain.Map
{
    public partial class Building
    {
        public static Building Unbesiegable { get; } = new UnbesiegableSpecialCase();

        class UnbesiegableSpecialCase : Building
        {
            public UnbesiegableSpecialCase() : base(int.MaxValue) { }

            public override bool IsBesiegable(Battalion besieger)
            {
                return false;
            }
        }
    }
}