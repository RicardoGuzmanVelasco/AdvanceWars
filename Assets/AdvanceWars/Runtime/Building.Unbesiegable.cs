namespace AdvanceWars.Runtime
{
    public partial class Building
    {
        internal static Building Unbesiegable { get; } = new UnbesiegableSpecialCase();

        public class UnbesiegableSpecialCase : Building
        {
            public UnbesiegableSpecialCase() : base(int.MaxValue) { }

            public override bool Equals(object obj)
            {
                if(obj is UnbesiegableSpecialCase)
                    return true;
                
                return base.Equals(obj);
            }
        }
    }
}