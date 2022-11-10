using System.Collections.Generic;
using AdvanceWars.Runtime;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;
using static AdvanceWars.Tests.Builders.UnitBuilder;

namespace AdvanceWars.Tests.Builders
{
    internal class SpawnerBuilder
    {
        int siegePoints = 0;
        Nation owner = Nation.Stateless;
        IEnumerable<UnitBuilder> units = new []{ Unit()};
        
        public static SpawnerBuilder Airfield() => new() { units = new List<UnitBuilder> { Unit().Of(Military.AirForce) }};
        public static SpawnerBuilder Barracks() => new() { units = new List<UnitBuilder> { Unit().Of(Military.Army) }};
        public static SpawnerBuilder Port() => new() { units = new List<UnitBuilder> { Unit().Of(Military.Navy) }};

        
        public static SpawnerBuilder Spawner()
        {
            return new SpawnerBuilder();
        }

        public SpawnerBuilder WithOwner(string ownerId) => WithOwner(new Nation(ownerId));

        public SpawnerBuilder WithUnits(params UnitBuilder[] units)
        {
            this.units = units;
            return this;
        }
        
        public SpawnerBuilder WithUnits(IEnumerable<UnitBuilder> units)
        {
            this.units = units;
            return this;
        }
        
        public SpawnerBuilder WithOwner(Nation owner)
        {
            this.owner = owner;

            return this;
        }

        public Spawner Build()
        {
            var spawner = new Spawner(siegePoints, owner);

            foreach (var unit in units) spawner.Add(unit.Build());
            
            return spawner;
        }
    }
}