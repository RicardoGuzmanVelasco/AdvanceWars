using System.Collections.Generic;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Spawner : Building
    {
        private List<Unit> spawnableUnits = new();

        internal override IEnumerable<Unit> SpawnableUnits => spawnableUnits;

        private Spawner(int siegePoints, Nation owner) : base(siegePoints, owner)
        {
        }

        private Spawner(int siegePoints) : base(siegePoints)
        {
        }

        public void Add(Unit unit)
        {
            Require(spawnableUnits).Not.Contains(unit);
            spawnableUnits.Add(unit);
        }

        public void Remove(Unit unit)
        {
            Require(spawnableUnits).Contains(unit);
            spawnableUnits.Remove(unit);
        }

        public static Spawner Barracks()
        {
            var barracks = new Spawner(1);
            barracks.Add(new Unit());
            return barracks;
        }

        public static Spawner Airfield()
        {
            var airfield = new Spawner(1);
            airfield.Add(new Unit {ServiceBranch = Military.AirForce});
            return airfield;
        }
    }
}