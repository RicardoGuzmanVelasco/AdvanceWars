using System.Linq;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.BattalionBuilder;
using static AdvanceWars.Tests.Builders.CommandingOfficerBuilder;
using static AdvanceWars.Tests.Builders.MapBuilder;
using static AdvanceWars.Tests.Builders.TerrainBuilder;

namespace AdvanceWars.Tests
{
    public class OperationTests
    {
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

            var sut = new Building(maxSiegePoints: 20);
            map.Put(new Vector2Int(0, 0), sut);

            var aNation = "aNation";
            var battalion = Battalion().WithNation(aNation).WithForces(10).Build();
            map.Put(new Vector2Int(0, 0), battalion);

            var commandingOfficers = new[]
            {
                CommandingOfficer().Build(),
                CommandingOfficer().WithMap(map).WithNation(aNation).Build()
            };
            var operation = new Operation(commandingOfficers, map);
            
            operation.EndTurn();

            battalion.Forces.Should().Be(30);
        }
        
        [Test]
        public void NonAllyBuilding_DoesNotHealBattalion_OnNewTurnBeginning()
        {
            var map = Map().Of(1,1).Build();

            var sut = new Building(maxSiegePoints: 20);
            map.Put(new Vector2Int(0, 0), sut);

            var initialForces = 1;
            var battalion = Battalion().WithNation("aNation").WithForces(initialForces).Build();
            map.Put(new Vector2Int(0, 0), battalion);

            var commandingOfficers = new[]
            {
                CommandingOfficer().Build(),
                CommandingOfficer().WithMap(map).WithNation("anotherNation").Build()
            };
            var operation = new Operation(commandingOfficers, map);
            
            operation.EndTurn();

            battalion.Forces.Should().Be(initialForces);
        }
        
        [Test]
        public void AllyBuilding_DoesNotOverhealBattalion_OnNewTurnBeginning()
        {
            var map = Map().Of(1,1).Build();

            var sut = new Building(maxSiegePoints: 20);
            map.Put(new Vector2Int(0, 0), sut);
            
            var aNation = "aNation";
            var battalion = Battalion().WithNation(aNation).Build();
            map.Put(new Vector2Int(0, 0), battalion);

            var commandingOfficers = new[]
            {
                CommandingOfficer().Build(),
                CommandingOfficer().WithMap(map).WithNation(aNation).Build()
            };
            var operation = new Operation(commandingOfficers, map);
            
            operation.EndTurn();

            battalion.Forces.Should().Be(100);
        }
        
        [Test]
        public void NonBuildingInSpace_DoesNotHealBattalion_OnNewTurnBeginning()
        {
            var map = Map().Of(1,1).Build();

            var nonBuildingTerrain = Terrain().Build();
            map.Put(new Vector2Int(0, 0), nonBuildingTerrain);
            
            var initialForces = 50;
            var aNation = "aNation";
            var battalion = Battalion().WithForces(initialForces).WithNation(aNation).Build();
            map.Put(new Vector2Int(0, 0), battalion);

            var commandingOfficers = new[]
            {
                CommandingOfficer().Build(),
                CommandingOfficer().WithMap(map).WithNation(aNation).Build()
            };
            var operation = new Operation(commandingOfficers, map);
            
            operation.EndTurn();

            battalion.Forces.Should().Be(initialForces);
        }

        [Test]
        public void AllyBuilding_ReplenishOccupantAmmo()
        {
            var map = Map().Of(1,1).Build();

            var building = new Building(maxSiegePoints: 20);
            map.Put(new Vector2Int(0, 0), building);
            
            var aNation = "aNation";
            var battalion = Battalion().WithNation(aNation).WithAmmo(5).Build();
            map.Put(new Vector2Int(0, 0), battalion);

            var commandingOfficers = new[]
            {
                CommandingOfficer().Build(),
                CommandingOfficer().WithMap(map).WithNation(aNation).Build()
            };
            var sut = new Operation(commandingOfficers, map);
            
            sut.EndTurn();

            battalion.AmmoRounds.Should().Be(7);
        }
    }
}