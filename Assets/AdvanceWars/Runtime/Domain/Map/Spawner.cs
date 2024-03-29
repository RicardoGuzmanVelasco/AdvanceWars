﻿using System.Collections.Generic;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Spawner : Building
    {
        private List<Unit> spawnableUnits = new();

        internal override IEnumerable<Unit> SpawnableUnits => spawnableUnits;

        public Spawner(int maxSiegePoints, Nation motherland) : base(maxSiegePoints, motherland) { }

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
    }
}