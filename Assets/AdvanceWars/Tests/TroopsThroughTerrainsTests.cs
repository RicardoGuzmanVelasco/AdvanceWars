using AdvanceWars.Runtime;
using AdvanceWars.Tests.Builders;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.TerrainBuilder;

namespace AdvanceWars.Tests
{
    public class TroopsThroughTerrainsTests
    {
        [Test]
        public void SpaceIsHostile_WhetherOccupiedBy_anEnemy()
        {
            var friend = BattalionBuilder.Infantry().Friend().Build();
            var space = new Map.Space();
            space.Occupy(BattalionBuilder.Infantry().Enemy().Build());
            var sut = space;

            sut.IsHostileTo(friend)
                .Should().BeTrue();
        }

        [Test]
        public void NonDefinedPropulsions_CostOne()
        {
            var sut = Terrain().WithCost(new Propulsion("NotA"), 2).Build();
            sut.MoveCostOf(new Propulsion("A")).Should().Be(1);
        }

        [Test]
        public void Unit_CannotCross_BlockerTerrain()
        {
            //Arrange
            var blockedProp = new Propulsion("Whatever");
            var unit = BattalionBuilder.Infantry().WithPropulsion(blockedProp).Build();
            var sut = new Map(1, 3);

            sut.Put(Vector2Int.zero, unit);
            sut.Put(Vector2Int.up, Terrain().WithBlocked(blockedProp).Build());

            //Act
            var result = sut.RangeOfMovement(unit);

            //Assert
            result.Should().BeEmpty();
        }
    }
}