using System.Collections.Generic;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Runtime
{
    public class Spawner : Building
    {
        internal override IEnumerable<Unit> SpawnableUnits { get; }

        private Spawner(int siegePoints, Nation owner) : base(siegePoints, owner)
        {
        }

        private Spawner(int siegePoints) : base(siegePoints)
        {
        }
        
        private Spawner(IEnumerable<Unit> spawnableUnits, int siegePoints) : this(siegePoints)
        {
            SpawnableUnits = spawnableUnits;
        }

        public static Spawner Barracks()
        {
            return new Spawner(new List<Unit>() {new Unit()}, 1);
        }

        public static Spawner Airfield()
        {
            return new Spawner(new List<Unit>() {new Unit {ServiceBranch = Military.AirForce}}, 1);
        }
    }
}