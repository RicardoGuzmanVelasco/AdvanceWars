using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.BattalionBuilder;

namespace AdvanceWars.Tests
{
    public class SpaceTests
    {
        [Test]
        public void BesiegingASpace_MaintainsTheSameBuilding()
        {
            var sut = new Map.Space();
            var building = new Building(siegePoints: 11);
            sut.Terrain = building;
            sut.Occupy(Battalion().WithPlatoons(8).Build());

            sut.Besiege();

            sut.Terrain.Should().BeSameAs(building);
        }

        [Test]
        public void WhenOccupantLeaves_SiegeIsLifted()
        {
            var sut = new Map.Space
            {
                Terrain = new Building(siegePoints: 11),
            };
            sut.Occupy(Battalion().WithPlatoons(8).Build());

            sut.Besiege();
            sut.Unoccupy();

            sut.Terrain.SiegePoints.Should().Be(11);
        }

        [Test]
        public void EnemyOccupant_CanBesiege()
        {
            var sut = new Map.Space();
            sut.Terrain = new Building(siegePoints: 11);
            sut.Occupy(Battalion().WithPlatoons(8).Build());

            sut.Besiege();

            sut.Terrain.SiegePoints.Should().Be(3);
        }

        [Test]
        public void BesiegableWhenBuildingIsEnemyOfOccupant()
        {
            var sut = new Map.Space
            {
                Terrain = new Building(default, new Nation("A"))
            };
            sut.Occupy(Battalion().WithNation("notA").Build());

            sut.IsBesiegable.Should().BeTrue();
        }

        [Test]
        public void BesiegableWhenBuildingIsNeutral()
        {
            var sut = new Map.Space
            {
                Terrain = new Building(default, Nation.Stateless)
            };
            sut.Occupy(Battalion().WithNation("Whatnot").Build());

            sut.IsBesiegable.Should().BeTrue();
        }

        [Test]
        public void UnbesiegableWhenBuildingInSpaceIsAllyOfOccupant()
        {
            var sut = new Map.Space
            {
                Terrain = new Building(default, new Nation("A"))
            };
            sut.Occupy(Battalion().WithNation("A").Build());

            sut.IsBesiegable.Should().BeFalse();
        }
    }
}