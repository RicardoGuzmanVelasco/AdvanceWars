using System;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;
using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.BattalionBuilder;
using static AdvanceWars.Tests.Builders.BuildingBuilder;
using Building = AdvanceWars.Runtime.Domain.Map.Building;

namespace AdvanceWars.Tests
{
    public class SpaceTests
    {
        [Test, Description("Solo para probar que se mantiene la referencia")]
        public void BesiegingASpace_MaintainsTheSameBuilding()
        {
            var building = Building().WithPoints(11).Build();
            var sut = new Map.Space
            {
                Terrain = building
            };
            sut.Occupy(Battalion().WithPlatoons(8).Build());

            sut.Besiege();

            sut.Terrain.Should().BeSameAs(building);
        }

        [Test]
        public void WhenOccupantLeaves_SiegeIsLifted()
        {
            var sut = new Map.Space
            {
                Terrain = Building().WithPoints(11).Build()
            };
            sut.Occupy(Battalion().WithPlatoons(8).Build());

            sut.Besiege();
            sut.Unoccupy();

            sut.Terrain.SiegePoints.Should().Be(11);
        }

        [Test]
        public void EnemyOccupant_CanBesiege()
        {
            var sut = new Map.Space
            {
                Terrain = Building().WithPoints(11).Build()
            };
            sut.Occupy(Battalion().WithPlatoons(8).Build());

            sut.Besiege();

            sut.Terrain.SiegePoints.Should().Be(3);
        }

        [Test]
        public void Besiegable_WhenBuilding_IsEnemyOfOccupant()
        {
            var sut = new Map.Space
            {
                Terrain = Building().WithNation("A").Build()
            };
            sut.Occupy(Battalion().WithNation("notA").Build());

            sut.IsBesiegable.Should().BeTrue();
        }

        [Test]
        public void Besiegable_WhenBuilding_IsNeutral()
        {
            var sut = new Map.Space
            {
                Terrain = Building().WithNation(Nation.Stateless).Build()
            };

            sut.Occupy(Battalion().Build());

            sut.IsBesiegable.Should().BeTrue();
        }

        [Test]
        public void Unbesiegable_WhenBuilding_IsAllyOfOccupant()
        {
            var sut = new Map.Space
            {
                Terrain = Building().WithNation("A").Build()
            };
            sut.Occupy(Battalion().WithNation("A").Build());

            sut.IsBesiegable.Should().BeFalse();
        }

        [Test]
        public void Unbesiegable_WhenBuilding_IsUnbesiegable()
        {
            var sut = new Map.Space
            {
                Terrain = Building.Unbesiegable
            };

            sut.IsBesiegable.Should().BeFalse();
        }

        [Test]
        public void anEnemyOccupant_Leaves_WithoutStartingAnySiege_OverTheBuilding()
        {
            var sut = new Map.Space
            {
                Terrain = Building().Build()
            };
            sut.Occupy(Battalion().Build());

            Action sutInvocation = () => sut.Unoccupy();

            sutInvocation.Should().NotThrow<Exception>();
        }
    }
}