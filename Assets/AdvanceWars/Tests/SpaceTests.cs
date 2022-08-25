using AdvanceWars.Runtime;
using AdvanceWars.Tests.Builders;
using FluentAssertions;
using NUnit.Framework;

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
            sut.Occupy(BattalionBuilder.Battalion().WithPlatoons(8).Build());

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
            sut.Occupy(BattalionBuilder.Battalion().WithPlatoons(8).Build());

            sut.Besiege();
            sut.Unoccupy();

            (sut.Terrain as Building).SiegePoints.Should().Be(11);
        }

        [Test]
        public void EnemyOccupant_CanBesiege()
        {
            var sut = new Map.Space();
            sut.Terrain = new Building(siegePoints: 11);
            sut.Occupy(BattalionBuilder.Battalion().WithPlatoons(8).Build());

            sut.Besiege();

            (sut.Terrain as Building).SiegePoints.Should().Be(3);
        }

        //hostil el terreno o por ocupante
    }
}