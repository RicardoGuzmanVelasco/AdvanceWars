using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime.Domain.Troops;
using UnityEngine;

namespace AdvanceWars.Runtime.Domain.Map
{
    public partial record Map
    {
        public static Map Null => new NoMap();

        record NoMap : Map, INull
        {
            // ReSharper disable once ConvertToPrimaryConstructor (Rider bug concerning records ctor)
            public NoMap() : base(0, 0) { }

            public override Space WhereIs(Battalion what)
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

            public override IEnumerable<Vector2Int> RangeOfMovement(Battalion battalion)
            {
                return Enumerable.Empty<Vector2Int>();
            }

            public override IEnumerable<Vector2Int> RangeOfMovement(Vector2Int @from, MovementRate rate)
            {
                return Enumerable.Empty<Vector2Int>();
            }
        }
    }
}