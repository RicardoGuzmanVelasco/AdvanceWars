using System.Linq;
using AdvanceWars.Runtime.Domain;
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
using Building = AdvanceWars.Runtime.Domain.Map.Building;
using Terrain = AdvanceWars.Runtime.Domain.Map.Terrain;

namespace AdvanceWars.Tests
{
    public class OperationTests
    {
        private Nation Ally => new Nation("Ally");
        private Nation Enemy => new Nation("Enemy");
        private Nation SomeNation => new Nation("A");
        private int SomeForces => 50;

        [Test]
        public void FirstActiveCommandingOfficer_IsTheFirst()
        {
            var officers = CommandingOfficers(1);
            var sut = new Operation(officers);

            sut.NationInTurn.Should().Be(officers.First().Motherland);
        }

        [Test]
        public void SecondCommandingOfficer_GoesAfterTheFirst()
        {
            var officers = CommandingOfficers(2);
            var sut = new Operation(officers);

            sut.EndTurn();

            sut.NationInTurn.Should().Be(officers[1].Motherland);
        }

        [Test]
        public void AfterLast_ActiveCommandingOfficerIsTheFirstAgain()
        {
            var officers = CommandingOfficers(2);
            var sut = new Operation(officers);

            sut.EndTurn();
            sut.EndTurn();

            sut.NationInTurn.Should().Be(officers.First().Motherland);
        }

        [Test]
        public void OperationStartsAtDayOne()
        {
            var sut = new Operation(CommandingOfficers(1));
            sut.Day.Should().Be(1);
        }

        [Test]
        public void WhenTurnOfEachNationEnds_aNewDayStarts()
        {
            var commandingOfficers = CommandingOfficers(2);
            var sut = new Operation(commandingOfficers);

            sut.EndTurn();
            sut.EndTurn();

            sut.Day.Should().Be(2);
        }
        
        [Test]
        public void AllyBuilding_HealsTwentyForcesToBattalion_OnNewTurnBeginning()
        {
            var map = Map().Of(1,1).Build();

            var sut = Building().WithNation(SomeNation).WithPoints(20).Build();
            map.Put(new Vector2Int(0, 0), sut);

            var battalion = Battalion().WithNation(SomeNation).WithForces(10).Build();
            map.Put(new Vector2Int(0, 0), battalion);

            var commandingOfficers = new[]
            {
                CommandingOfficer().Build(),
                CommandingOfficer().WithMap(map).WithNation(SomeNation).Build()
            };
            var operation = new Operation(commandingOfficers, map);
            
            operation.EndTurn();

            battalion.Forces.Should().Be(30);
        }
        
        [Test]
        public void NonAllyBuilding_DoesNotHealBattalion_OnNewTurnBeginning()
        {
            var map = Map().Of(1,1).Build();
            var sut =  Building().WithNation(Enemy).WithPoints(20).Build();
            map.Put(new Vector2Int(0, 0), sut);
            var battalion = Battalion().WithNation(Enemy).WithForces(SomeForces).Build();
            map.Put(new Vector2Int(0, 0), battalion);

            var commandingOfficers = new[]
            {
                CommandingOfficer().Build(),
                CommandingOfficer().WithMap(map).WithNation(Ally).Build()
            };
            var operation = new Operation(commandingOfficers, map);
            
            operation.EndTurn();

            battalion.Forces.Should().Be(SomeForces);
        }

        [Test, Category("Regression")]
        public void AllyBuilding_DoesNotHealEnemies()
        {
            var map = new Map(1, 1);
            map.Put(Vector2Int.zero, Building().WithNation(Ally).Build());
            var enemyBattalion = Battalion().WithForces(1).WithNation(Enemy).Build();
            map.Put(Vector2Int.zero, enemyBattalion);
            var sut = CommandingOfficer().WithNation(Ally).WithMap(map).Build();
            
            sut.BeginTurn();

            enemyBattalion.Forces.Should().Be(1);
        }

        [Test, Category("Regression")]
        public void AllyBuilding_DoesNotReplenishEnemyAmmo()
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
        public void NonBuildingInSpace_DoesNotHealBattalion_OnNewTurnBeginning()
        {
            var map = Map().Of(1,1).Build();
            map.Put(new Vector2Int(0, 0), Terrain().Build());
            var battalion = Battalion().WithForces(SomeForces).WithNation(SomeNation).Build();
            map.Put(new Vector2Int(0, 0), battalion);

            var commandingOfficers = new[]
            {
                CommandingOfficer().Build(),
                CommandingOfficer().WithMap(map).WithNation(SomeNation).Build()
            };
            var sut = new Operation(commandingOfficers, map);
            
            sut.EndTurn();

            battalion.Forces.Should().Be(SomeForces);
        }

        [Test]
        public void AllyBuilding_ReplenishOccupantAmmo()
        {
            var map = Map().Of(1,1).Build();
            var building = BuildingBuilder.Building().WithNation(SomeNation).WithPoints(20).Build();
            map.Put(new Vector2Int(0, 0), building);
            var battalion = Battalion().WithNation(SomeNation).WithAmmo(5).Build();
            map.Put(new Vector2Int(0, 0), battalion);
            
            var commandingOfficers = new[]
            {
                CommandingOfficer().Build(),
                CommandingOfficer().WithMap(map).WithNation(SomeNation).Build()
            };
            var sut = new Operation(commandingOfficers, map);
            
            sut.EndTurn();

            battalion.AmmoRounds.Should().Be(7);
        }
    }
}