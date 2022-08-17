using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.BatallionBuilder;
using static AdvanceWars.Tests.Builders.TerrainBuilder;

namespace AdvanceWars.Tests
{
    public class MapTests
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
        public void RangeOfMovement_DoesNotReturnOccupiedPosition()
        {
            var sut = new Map(2, 2);
            sut.Put(Vector2Int.up, Batallion().Build());

            var result = sut.RangeOfMovement(from: Vector2Int.zero, rate: 1);

            result.Should().NotContain(Vector2Int.up);
        }

        [Test]
        public void UnitRangeOfMovement()
        {
            var unit = Batallion().Build();
            var sut = new Map(3, 3);
            sut.Put(Vector2Int.one, unit);

            var result = sut.RangeOfMovement(unit);

            result.Should().NotBeEmpty();
        }

        [Test]
        public void EnemyUnitBlocksTheWay()
        {
            var friend = Infantry().Friend().Build();
            var sut = new Map(1, 3);
            sut.Put(Vector2Int.zero, friend);
            sut.Put(Vector2Int.up, Infantry().Enemy().Build());

            sut.RangeOfMovement(friend)
                .Should().BeEmpty();
        }

        [Test]
        public void SpaceIsHostile_WhetherOccupiedBy_anEnemy()
        {
            var friend = Infantry().Friend().Build();
            var sut = new Map.Space { Occupant = Infantry().Enemy().Build() };

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
        public void RangeOfMovement_DelimitedBy_UnitMoveRate()
        {
            var sut = new Map(1, 6);
            var unit = Batallion().WithMoveRate(2).Build();
            sut.Put(Vector2Int.zero, unit);

            var result = sut.RangeOfMovement(unit);

            result.Should().HaveCount(2);
        }

        [Test]
        public void Unit_CannotCross_BlockerTerrain()
        {
            //Arrange
            var blockedProp = new Propulsion("Whatever");
            var unit = Infantry().WithPropulsion(blockedProp).Build();
            var sut = new Map(1, 3);

            sut.Put(Vector2Int.zero, unit);
            sut.Put(Vector2Int.up, Terrain().WithBlocked(blockedProp).Build());

            //Act
            var result = sut.RangeOfMovement(unit);

            //Assert
            result.Should().BeEmpty();
        }

        //no exponer unit en batallion
        //eliminar el problema de tener que estar comprobando si existe el espacio.
        //O bien iniciar los espacios o bien estructura para que se inicien lazy al acceder a ellos.
        //el caso de blocker ahora mismo está mockeado.
        //public space at
        //no deberia de haber set en el terreno
        //Movement Cost
        //duplicacion espacios y bounds. Nullcheck espacio en range of movement
    }
}