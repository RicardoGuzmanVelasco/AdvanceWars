using AdvanceWars.Runtime;
using AdvanceWars.Tests.Builders;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace AdvanceWars.Tests
{
    public class TroopsThroughTerrainsTests
    {
        [Test]
        public void SpaceIsHostile_WhetherOccupiedBy_anEnemy()
        {
            var friend = BatallionBuilder.Infantry().Friend().Build();
            var sut = new Map.Space { Occupant = BatallionBuilder.Infantry().Enemy().Build() };

            sut.IsHostileTo(friend)
                .Should().BeTrue();
        }

        [Test]
        public void NonDefinedPropulsions_CostOne()
        {
            var sut = TerrainBuilder.Terrain().WithCost(new Propulsion("NotA"), 2).Build();
            sut.MoveCostOf(new Propulsion("A")).Should().Be(1);
        }

        [Test]
        public void Unit_CannotCross_BlockerTerrain()
        {
            //Arrange
            var blockedProp = new Propulsion("Whatever");
            var unit = BatallionBuilder.Infantry().WithPropulsion(blockedProp).Build();
            var sut = new Map(1, 3);

            sut.Put(Vector2Int.zero, unit);
            sut.Put(Vector2Int.up, TerrainBuilder.Terrain().WithBlocked(blockedProp).Build());

            //Act
            var result = sut.RangeOfMovement(unit);

            //Assert
            result.Should().BeEmpty();
        }
    }
}