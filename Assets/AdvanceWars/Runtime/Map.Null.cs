using System.Collections.Generic;
using System.Linq;

namespace AdvanceWars.Runtime
{
    public partial record Map
    {
        public static Map Null { get; } = new NoMap();

        record NoMap : Map, INull
        {
            // ReSharper disable once ConvertToPrimaryConstructor (Rider bug concerning records ctor)
            public NoMap() : base(0, 0) { }

            public override Space WhereIs(Allegiance what)
            {
                return Space.Null;
            }

            public override string ToString()
            {
                return this.GetType().Name;
            }

            public override IEnumerable<Battalion> EnemyBattalionsInRangeOfFire(Battalion battalion)
            {
                return Enumerable.Empty<Battalion>();
            }
        }
    }
}