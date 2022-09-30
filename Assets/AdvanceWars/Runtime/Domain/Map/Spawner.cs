using System.Collections.Generic;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Runtime
{
    public class Spawner : Building
    {
        internal override IEnumerable<Unit> SpawnableUnits => new List<Unit>() { new Unit()};
        public Spawner(int siegePoints, Nation owner) : base(siegePoints, owner)
        {
        }

        public Spawner(int siegePoints) : base(siegePoints)
        {
        }

        public static Spawner Barracks()
        {
            return new Spawner(1);
        }
    }
}