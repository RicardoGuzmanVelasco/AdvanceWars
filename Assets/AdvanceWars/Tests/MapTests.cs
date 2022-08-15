using System.Collections.Generic;
using AdvanceWars.Runtime;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.BatallionBuilder;
using Terrain = AdvanceWars.Runtime.Terrain;

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
            sut.Put(Vector2Int.up, Batallion().Build());

            var result = sut.RangeOfMovement(Vector2Int.zero, 1);

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
            var sut = new Terrain(new Dictionary<Propulsion, int> { { new Propulsion("NotA"), 2 } });
            var propulsion = new Propulsion("A");
            sut.MoveCostOf(propulsion).Should().Be(1);
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
            var unit = Infantry().WithPropulsion("A").Build();
            var sut = new Map(1, 3);

            sut.Put(Vector2Int.zero, unit);
            sut.Put(Vector2Int.up, new Terrain(new[] { new Propulsion("A") }));

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
        //Movement Type es Propulsion
        //Movement Rate es Mobility
        //duplicacion espacios y bounds. Nullcheck espacio en range of movement
        //coger el coste según el tipo en el terreno
        //cosas de costes
    }
}