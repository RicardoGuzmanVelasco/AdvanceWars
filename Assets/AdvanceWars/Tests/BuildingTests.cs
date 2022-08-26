using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.BattalionBuilder;

namespace AdvanceWars.Tests
{
    public class BuildingTests
    {
        [Test]
        public void NeutralBuildingReceiveSiegeDamage()
        {
            var sut = new Building(siegePoints: 20);
            var battalion = Battalion().WithNation("A").WithPlatoons(1).Build();

            var result = sut.SiegeOutcome(besieger: battalion);

            result.SiegePoints.Should().Be(19);
            result.RelationshipWith(battalion).Should().Be(DiplomaticRelation.Neutral);
        }

        [Test]
        public void EnemyBuildingReceiveSiegeDamage()
        {
            var sut = new Building(siegePoints: 20, owner: new Nation("Enemy"));
            var battalion = Battalion().WithNation("Ally").WithPlatoons(1).Build();

            var result = sut.SiegeOutcome(besieger: battalion);

            result.SiegePoints.Should().Be(19);
            result.RelationshipWith(battalion).Should().Be(DiplomaticRelation.Enemy);
        }

        [Test]
        public void BuildingIsCaptured_WhenSiegePointsDropBelowZero()
        {
            var sut = new Building(siegePoints: 20);
            var battalion = Battalion().WithNation("A").WithInfiniteForces().Build();

            var result = sut.SiegeOutcome(besieger: battalion);

            result.SiegePoints.Should().Be(20);
            result.RelationshipWith(battalion).Should().Be(DiplomaticRelation.Ally);
        }

        [Test]
        public void BuildingIsCaptured_WhenSiegePointsDropToZero()
        {
            var sut = new Building(siegePoints: 20);
            var battalion = Battalion().WithNation("A").WithPlatoons(20).Build();

            var result = sut.SiegeOutcome(besieger: battalion);

            result.SiegePoints.Should().Be(20);
            result.RelationshipWith(battalion).Should().Be(DiplomaticRelation.Ally);
        }
    }
}