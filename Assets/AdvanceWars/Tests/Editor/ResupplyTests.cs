using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;
using AdvanceWars.Tests.Builders;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.BattalionBuilder;
using static AdvanceWars.Tests.Builders.BuildingBuilder;
using static AdvanceWars.Tests.Builders.CommandingOfficerBuilder;
using static AdvanceWars.Tests.Builders.MapBuilder;
using static AdvanceWars.Tests.Builders.TerrainBuilder;

namespace AdvanceWars.Tests
{
    public class ResupplyTests
    {
        private Nation Ally => new Nation("Ally");
        private Nation Enemy => new Nation("Enemy");
        private Nation SomeNation => new Nation("A");
        private int SomeForces => 50;

        [Test]
        public void AllyBuilding_DoesNotHealEnemies_OnNewTurnBeginning()
        {
            var map = new Map(1, 1);
            map.Put(Vector2Int.zero, Building().WithNation(Ally).Build());
            var enemyBattalion = Battalion().WithForces(1).WithNation(Enemy).Build();
            map.Put(Vector2Int.zero, enemyBattalion);
            var sut = CommandingOfficer().WithNation(Ally).WithMap(map).Build();

            sut.BeginTurn();

            enemyBattalion.Forces.Value.Should().Be(1);
        }

        [Test]
        public void AllyBuilding_DoesNotReplenishEnemyAmmo_OnNewTurnBeginning()
        {
            var map = new Map(1, 1);
            map.Put(Vector2Int.zero, Building().WithNation(Ally).Build());
            var enemyBattalion = Battalion().WithAmmo(1).WithNation(Enemy).Build();
            map.Put(Vector2Int.zero, enemyBattalion);
            var sut = CommandingOfficer().WithNation(Ally).WithMap(map).Build();

            sut.BeginTurn();

            enemyBattalion.AmmoRounds.Should().Be(1);
        }

        [Test]
        public void AllyBuilding_HealsBattalionFromSameMilitary()
        {
            var map = Map().Of(1, 1).Build();
            var building = Barracks().WithNation(SomeNation).Build();
            map.Put(new Vector2Int(0, 0), building);
            var battalion = Infantry().WithNation(SomeNation).WithForces(10).Build();
            map.Put(new Vector2Int(0, 0), battalion);
            var sut = CommandingOfficer().WithMap(map).WithNation(SomeNation).Build();

            sut.BeginTurn();

            battalion.Forces.Value.Should().Be(30);
        }

        //Idem
        [Test]
        public void NonAllyBuilding_DoesNotHealBattalion_OnNewTurnBeginning()
        {
            var map = Map().Of(1, 1).Build();
            var building = Building().WithNation(Enemy).Build();
            map.Put(new Vector2Int(0, 0), building);
            var battalion = Battalion().WithNation(Enemy).WithForces(SomeForces).Build();
            map.Put(new Vector2Int(0, 0), battalion);
            var sut = CommandingOfficer().WithMap(map).WithNation(Ally).Build();

            sut.BeginTurn();

            battalion.Forces.Value.Should().Be(SomeForces);
        }

        [Test]
        public void NonBuildingInSpace_DoesNotHealBattalion_OnNewTurnBeginning()
        {
            var map = Map().Of(1, 1).Build();
            map.Put(new Vector2Int(0, 0), Terrain().Build());
            var battalion = Battalion().WithForces(SomeForces).WithNation(SomeNation).Build();
            map.Put(new Vector2Int(0, 0), battalion);
            var sut = CommandingOfficer().WithMap(map).WithNation(SomeNation).Build();

            sut.BeginTurn();

            battalion.Forces.Value.Should().Be(SomeForces);
        }

        [Test]
        public void AllyBuilding_ReplenishAllyAmmo()
        {
            var battalion = Battalion().WithNation(SomeNation).WithAmmo(5).Build();
            var sut = new Map.Space()
            {
                Terrain = Building().WithNation(SomeNation).Build()
            };
            sut.Occupy(battalion);

            sut.ReplenishOccupantAmmo();

            battalion.AmmoRounds.Should().Be(7);
        }


        [Test]
        public void AllyBuilding_HealsPartially_WhenNotEnoughFunds()
        {
            var map = Map().Of(1, 1).Build();
            var building = Building().WithNation(SomeNation).Build();
            map.Put(new Vector2Int(0, 0), building);
            var battalion = Battalion().WithNation(SomeNation).WithForces(85).WithPrice(1000).Build();
            map.Put(new Vector2Int(0, 0), battalion);
            var sut = SituationBuilder.Situation().WithMap(map).WithNation(SomeNation).WithWarFunds(100).Build();

            sut.ManageLogistics();

            battalion.Forces.Value.Should().Be(95);
            sut.WarFunds.Should().Be(0);
        }

        [Test]
        public void BuildingDoesNotHeal_OtherMilitaryBattalions()
        {
            var map = Map().Of(1, 1).Build();
            var building = Airfield().WithNation(SomeNation).Build();
            map.Put(new Vector2Int(0, 0), building);
            var battalion = Infantry().WithNation(SomeNation).WithForces(10).Build();
            map.Put(new Vector2Int(0, 0), battalion);
            var sut = SituationBuilder.Situation().WithMap(map).WithNation(SomeNation).Build();

            sut.ManageLogistics();

            battalion.Forces.Value.Should().Be(10);
        }
    }
}