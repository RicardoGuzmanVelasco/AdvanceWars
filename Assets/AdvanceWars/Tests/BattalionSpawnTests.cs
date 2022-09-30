using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;
using AdvanceWars.Tests.Builders;
using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.SpawnerBuilder;
using static AdvanceWars.Tests.Builders.UnitBuilder;
using Unit = AdvanceWars.Runtime.Domain.Troops.Unit;

namespace AdvanceWars.Tests
{
    public class BattalionSpawnTests
    {
        [Test]
        public void Space_WithSpawner_CanSpawnUnits()
        {
            var spawner = Spawner().Build();
            
            var sut = new Map.Space {Terrain = spawner};
            
            sut.SpawnableUnits.Should().NotBeEmpty();
        }
        
        [Test]
        public void Space_WithoutSpawner_CanNotSpawnUnits()
        {
            var sut = new Map.Space();
            
            sut.SpawnableUnits.Should().BeEmpty();
        }

        [Test]
        public void Space_WithSpawner_SpawnsBattalion()
        {
            var spawner = Barracks().Build();
            var sut = new Map.Space {Terrain = spawner};
            
            sut.SpawnBattalionHere(Unit().Of(Military.Army).Build());

            sut.IsOccupied.Should().BeTrue();
        }

        [Test]
        public void Airfield_OnlySpawns_UnitsFromTheAirForce()
        {
            var airfield = Airfield().Build();
            var sut = new Map.Space {Terrain = airfield};
            
            sut.SpawnableUnits.
                Should().AllSatisfy(x => x.ServiceBranch.Should().Be(Military.AirForce));
        }

        [Test]
        public void SpawnedBattalionAllegiance_IsTheSameAs_SpawnerAllegiance()
        {
            var spawner = Airfield().WithOwner("anyNation").Build();
            var sut = new Map.Space {Terrain = spawner};

            sut.SpawnBattalionHere(Unit().Of(Military.AirForce).Build());

            sut.Occupant.IsAlly(spawner).Should().BeTrue();
        }
    }
}