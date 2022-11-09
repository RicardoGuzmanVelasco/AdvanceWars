using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Orders.Maneuvers;
using AdvanceWars.Runtime.Domain.Troops;
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
            var battalion = Battalion().WithNation("ally").Build();
            var building = new Building(maxSiegePoints: 20, owner: new Nation("enemy"));

            var map = new Map(1, 1);
            map.Put(Vector2Int.zero, building);
            map.Put(Vector2Int.zero, battalion);

            var sut = CommandingOfficer().WithNation("ally").WithMap(map).Build();

            sut.AvailableTacticsAt(map.WhereIs(battalion)!)
                .Should().Contain(Tactic.Siege);
        }

        [Test]
        public void SiegeTacticIsNotAvailable_WhenBuildingIsNotBesiegableByBattalion()
        {
            var battalion = Battalion().WithNation("sameNation").Build();
            var building = new Building(maxSiegePoints: 0, owner: new Nation("sameNation"));

            var map = new Map(1, 1);
            map.Put(Vector2Int.zero, building);
            map.Put(Vector2Int.zero, battalion);

            var sut = CommandingOfficer().WithNation("sameNation").WithMap(map).Build();

            sut.AvailableTacticsAt(map.WhereIs(battalion)!)
                .Should().NotContain(Tactic.Siege);
        }

        [Test]
        public void SiegeManeuverIsApplied()
        {
            var battalion = Battalion().WithNation("ally").WithPlatoons(1).Build();
            var building = new Building(maxSiegePoints: 20, owner: new Nation("enemy"));

            var map = new Map(1, 1);
            map.Put(Vector2Int.zero, building);
            map.Put(Vector2Int.zero, battalion);

            var sut = CommandingOfficer().WithNation("ally").WithMap(map).Build();

            sut.Order(Maneuver.Siege(battalion));

            building.SiegePoints
                .Should().Be(19);
        }
    }
}