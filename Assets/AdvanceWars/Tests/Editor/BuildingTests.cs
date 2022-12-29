﻿using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;
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
            var sut = new Building(maxSiegePoints: 20);
            var battalion = Battalion().WithNation("A").WithPlatoons(1).Build();

            var result = sut.SiegeOutcome(besieger: battalion);

            result.SiegePoints.Value.Should().Be(19);
            result.IsNeutral(battalion).Should().BeTrue();
        }

        [Test]
        public void EnemyBuildingReceiveSiegeDamage()
        {
            var sut = new Building(maxSiegePoints: 20, motherland: new Nation("Enemy"));
            var battalion = Battalion().WithNation("Ally").WithPlatoons(1).Build();

            var result = sut.SiegeOutcome(besieger: battalion);

            result.SiegePoints.Value.Should().Be(19);
            result.IsEnemy(battalion).Should().BeTrue();
        }

        [Test]
        public void BuildingIsCaptured_WhenSiegePointsDropBelowZero()
        {
            var sut = new Building(maxSiegePoints: 20);
            sut.SiegePoints = 1;
            var battalion = Battalion().WithNation("A").WithMaxForces().Build();

            var result = sut.SiegeOutcome(besieger: battalion);

            result.SiegePoints.Value.Should().Be(20);
            result.IsAlly(battalion).Should().BeTrue();
        }

        [Test]
        public void BuildingIsCaptured_WhenSiegePointsDropToZero()
        {
            var sut = new Building(maxSiegePoints: 20);
            sut.SiegePoints = 10;
            var battalion = Battalion().WithNation("A").WithPlatoons(10).Build();

            var result = sut.SiegeOutcome(besieger: battalion);

            result.SiegePoints.Value.Should().Be(20);
            result.IsAlly(battalion).Should().BeTrue();
        }
    }
}