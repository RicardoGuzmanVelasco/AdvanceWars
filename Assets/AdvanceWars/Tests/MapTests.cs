using AdvanceWars.Runtime;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using UnityEngine;

namespace AdvanceWars.Tests
{
    public class MapTests
    {
        [Test]
        public void Map_WithSizeOne_ReturnsEmpty()
        {
            var sut = new Map(1, 1);
            var result = sut.RangeOfMovement(Vector2Int.zero, 1);
            result.Should().BeEmpty();
        }

        [Test]
        public void RangeOfMovement_WhenRateIsZero_ReturnsEmpty()
        {
            var sut = new Map(1, 2);
            var result = sut.RangeOfMovement(Vector2Int.zero, 0);
            result.Should().BeEmpty();
        }

        [Test]
        public void RangeOfMovement_WhenRateIsGreaterThanZero_DoesNotReturnEmpty()
        {
            var sut = new Map(1, 2);
            var result = sut.RangeOfMovement(Vector2Int.zero, 1);
            result.Should().NotBeEmpty();
        }

        [Test]
        public void RangeOfMovement_WhenRangeIsOne_DoesNotReturnDiagonalTiles()
        {
            var sut = new Map(2, 2);
            var result = sut.RangeOfMovement(Vector2Int.zero, 1);
            result.Should().NotContain(Vector2Int.one);
        }

        [Test]
        public void RangeOfMovement_WhenRangeIsTwo_ReturnsDiagonalTiles()
        {
            var sut = new Map(2, 2);
            var result = sut.RangeOfMovement(Vector2Int.zero, 2);
            result.Should().Contain(Vector2Int.one);
        }

        [Test]
        public void RangeOfMovement_WhenRangeIsOne_ReturnsAdjacents()
        {
            var sut = new Map(2, 2);
            var result = sut.RangeOfMovement(Vector2Int.zero, 1);

            using var _ = new AssertionScope();
            result.Should().Contain(Vector2Int.up);
            result.Should().Contain(Vector2Int.right);
        }

        [Test]
        public void RangeOfMovement_WhenRangeIsOne_DoesNotReturnSelfPosition()
        {
            var sut = new Map(2, 2);
            var result = sut.RangeOfMovement(Vector2Int.zero, 1);
            result.Should().NotContain(Vector2Int.zero);
        }

        [Test]
        public void RangeOfMovement_DoesNotReturnOccupiedPosition()
        {
            var sut = new Map(2, 2);
            sut.Occupy(Vector2Int.up, new Unit());

            var result = sut.RangeOfMovement(Vector2Int.zero, 1);

            result.Should().NotContain(Vector2Int.up);
        }

        [Test]
        public void UnitRangeOfMovement()
        {
            var unit = new Unit();
            var sut = new Map(3, 3);
            sut.Occupy(Vector2Int.one, unit);

            var result = sut.RangeOfMovement(unit);

            result.Should().NotBeEmpty();
        }

        [Test, Ignore("TODO")]
        public void EnemyUnitBlocksTheWay()
        {
            var friend = new Unit { Motherland = new Nation("Friend") };
            var enemy = new Unit { Motherland = new Nation("Enemy") };
            var sut = new Map(1, 3);
            sut.Occupy(Vector2Int.zero, friend);
            sut.Occupy(Vector2Int.up, enemy);

            var result = sut.RangeOfMovement(friend);

            result.Should().BeEmpty();
        }

        [Test]
        public void SpaceIsHostile_WhetherOccupiedBy_anEnemy()
        {
            var friend = new Unit { Motherland = new Nation("Friend") };
            var enemy = new Unit { Motherland = new Nation("Enemy") };
            var sut = new Map.Space { Occupant = enemy };
            sut.IsHostileTo(friend).Should().BeTrue();
        }

        //las unidades enemigas, bloquean
        //coger el coste según el tipo en el terreno
        //el mapa tiene spaces
        //
        //cosas de costes
        //
    }
}