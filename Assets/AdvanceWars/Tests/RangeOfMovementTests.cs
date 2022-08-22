using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.BattalionBuilder;

namespace AdvanceWars.Tests
{
    public class RangeOfMovementTests
    {
        [Test]
        public void Map_WithSizeOne_HasNoRangeOfMovement()
        {
            new Map(1, 1)
                .RangeOfMovement(from: Vector2Int.zero, rate: 1)
                .Should().BeEmpty();
        }

        [Test]
        public void IfNoMovementRate_ThenNoRangeOfMovement()
        {
            new Map(1, 2)
                .RangeOfMovement(from: Vector2Int.zero, rate: 0)
                .Should().BeEmpty();
        }

        [Test]
        public void IfThereIsSpaceAndMovementRate_ThenThereIsRangeOfMovement()
        {
            new Map(1, 2)
                .RangeOfMovement(from: Vector2Int.zero, rate: 1)
                .Should().NotBeEmpty();
        }

        [Test]
        public void WithMovementRate1_RangeOfMovementDoesNotIncludeDiagonals()
        {
            new Map(2, 2)
                .RangeOfMovement(from: Vector2Int.zero, rate: 1)
                .Should().NotContain(Vector2Int.one);
        }

        [Test]
        public void WithMovementRateGreaterThan1_RangeOfMovementIncludesDiagonals()
        {
            new Map(2, 2)
                .RangeOfMovement(from: Vector2Int.zero, rate: 2)
                .Should().Contain(Vector2Int.one);
        }

        [Test]
        public void IfThereIsSpaceAndMovementRate_ThenRangeOfMovementIncludesAdjacents()
        {
            new Map(2, 2)
                .RangeOfMovement(from: Vector2Int.zero, rate: 1)
                .Should()
                .Contain(Vector2Int.up).And.Contain(Vector2Int.right);
        }

        [Test]
        public void RangeOfMovement_NeverIncludesOriginalPosition()
        {
            new Map(2, 2)
                .RangeOfMovement(from: Vector2Int.zero, rate: 1)
                .Should().NotContain(Vector2Int.zero);

            new Map(3, 3)
                .RangeOfMovement(from: Vector2Int.one, rate: 4)
                .Should().NotContain(Vector2Int.one);
        }

        [Test]
        public void RangeOfMovement_NeverIncludes_anOccupiedPosition()
        {
            var occupiedPos = Vector2Int.up;
            var sut = new Map(2, 2);
            sut.Put(occupiedPos, Battalion().Build());

            sut.RangeOfMovement(from: Vector2Int.zero, rate: 1)
                .Should().NotContain(occupiedPos);
        }

        [Test]
        public void RangeOfMovement_isAlsoComputable_FromTroops()
        {
            var troop = Infantry().Build();
            var sut = new Map(3, 3);
            sut.Put(Vector2Int.one, troop);

            sut.RangeOfMovement(troop)
                .Should().NotBeEmpty();
        }

        [Test]
        public void EnemyBlocksTheWay_SoRangeOfMovementStopsOnEnemy()
        {
            var aTroop = Infantry().Friend().Build();
            var sut = new Map(1, 3);
            sut.Put(Vector2Int.zero, aTroop);
            sut.Put(Vector2Int.up, Infantry().Enemy().Build());

            sut.RangeOfMovement(aTroop)
                .Should().BeEmpty();
        }

        [Test]
        public void MovementRate_Limits_RangeOfMovement()
        {
            var troop = Battalion().WithMoveRate(2).Build();
            var sut = new Map(1, 6);
            sut.Put(Vector2Int.zero, troop);

            sut.RangeOfMovement(troop)
                .Should().HaveCount(2);
        }
    }
}