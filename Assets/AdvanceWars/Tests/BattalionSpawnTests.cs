using AdvanceWars.Runtime;
using AdvanceWars.Runtime.Domain.Map;
using FluentAssertions;
using NUnit.Framework;

namespace AdvanceWars.Tests
{
    public class BattalionSpawnTests
    {
        [Test]
        public void Space_WithSpawner_CanSpawnUnits()
        {
            var spawner = Spawner.Barracks();
            var sut = new Map.Space {Terrain = spawner};
            sut.SpawnableUnits.Should().NotBeEmpty();
        }
        
        [Test]
        public void Space_WithoutSpawner_CanNotSpawnUnits()
        {
            var sut = new Map.Space();
            sut.SpawnableUnits.Should().BeEmpty();
        }
    }
}