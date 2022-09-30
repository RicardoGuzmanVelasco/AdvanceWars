using System.Linq;
using AdvanceWars.Runtime;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;
using AdvanceWars.Tests.Builders;
using FluentAssertions;
using NUnit.Framework;

namespace AdvanceWars.Tests
{
    public class BattalionSpawnTests
    {
        [Test]
        public void Space_WithSpawner_CanSpawnUnits()
        {
            var spawner = Spawner.Barracks(Nation.Stateless);
            
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
            var spawner = Spawner.Barracks(Nation.Stateless);
            var sut = new Map.Space {Terrain = spawner};
            
            sut.SpawnBattalionHere(new Unit());

            sut.IsOccupied.Should().BeTrue();
        }

        [Test]
        public void Airfield_OnlySpawns_UnitsFromTheAirForce()
        {
            var airfield = Spawner.Airfield();
            var sut = new Map.Space {Terrain = airfield};
            
            sut.SpawnableUnits.
                Should().AllSatisfy(x => x.ServiceBranch.Should().Be(Military.AirForce));
        }

        [Test]
        public void SpawnedBattalionAllegiance_IsTheSameAs_SpawnerAllegiance()
        {
            var spawner = Spawner.Barracks(new Nation("anyNation"));
            var sut = new Map.Space {Terrain = spawner};

            sut.SpawnBattalionHere(new Unit());

            sut.Occupant.IsAlly(spawner).Should().BeTrue();
        }
    }
}