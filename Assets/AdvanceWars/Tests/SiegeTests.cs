using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.BattalionBuilder;
using static AdvanceWars.Tests.Builders.CommandingOfficerBuilder;

namespace AdvanceWars.Tests
{
    public class SiegeTests
    {
        [Test]
        public void SiegeTacticIsAvailable_WhenBuildingIsBesiegableByBattalion()
        {
            var battalion = Battalion().WithNation("aNation").Build();
            var building = new Building(siegePoints: 20, owner: new Nation("anotherNation"));

            var map = new Map(1, 1);
            map.Put(Vector2Int.zero, building);
            map.Put(Vector2Int.zero, battalion);

            var sut = CommandingOfficer().WithMap(map).Build();

            sut.AvailableTacticsOf(battalion)
                .Should().Contain(Tactic.Siege);
        }

        [Test]
        public void SiegeTacticIsNotAvailable_WhenBuildingIsNotBesiegableByBattalion()
        {
            var battalion = Battalion().WithNation("sameNation").Build();
            var building = new Building(siegePoints: 0, owner: new Nation("sameNation"));

            var map = new Map(1, 1);
            map.Put(Vector2Int.zero, building);
            map.Put(Vector2Int.zero, battalion);

            var sut = CommandingOfficer().WithMap(map).Build();

            sut.AvailableTacticsOf(battalion)
                .Should().NotContain(Tactic.Siege);
        }
    }
}