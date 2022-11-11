using System.Collections.Generic;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Orders.Maneuvers;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.BattalionBuilder;
using static AdvanceWars.Tests.Builders.CommandingOfficerBuilder;
using static AdvanceWars.Tests.Builders.SituationBuilder;
using Battalion = AdvanceWars.Runtime.Domain.Troops.Battalion;

namespace AdvanceWars.Tests
{
    public class BattalionMergeTests
    {
        [Test]
        public void MoveTactic_Available_WhenBattalionsCanJoin()
        {
            var fullHealthAllyBattalion = Battalion().WithNation("sameNation").WithForces(100).WithMoveRate(1).Build();
            var damagedAllyBattalion = Battalion().WithNation("sameNation").WithForces(10).WithMoveRate(1).Build();
            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, fullHealthAllyBattalion);
            map.Put(Vector2Int.up, damagedAllyBattalion);
            var sut = CommandingOfficer().WithNation("sameNation").WithMap(map).Build();

            sut.AvailableTacticsAt(map.WhereIs(fullHealthAllyBattalion)!)
                .Should().Contain(Tactic.Move);
        }

        [Test]
        public void MoveTactic_NotAvailable_WhenBattalionsCanNotJoin()
        {
            var damagedAllyBattalion = Battalion().WithNation("sameNation").WithForces(10).WithMoveRate(1).Build();
            var fullHealthAllyBattalion = Battalion().WithNation("sameNation").WithForces(100).WithMoveRate(1).Build();
            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, damagedAllyBattalion);
            map.Put(Vector2Int.up, fullHealthAllyBattalion);
            var sut = CommandingOfficer().WithNation("sameNation").WithMap(map).Build();

            sut.AvailableTacticsAt(map.WhereIs(damagedAllyBattalion)!)
                .Should().NotContain(Tactic.Move);
        }

        [Test]
        public void MoveTactic_NotAvailable_WhenBattalionsAreEnemies()
        {
            var allyBattalion = Battalion().WithNation("sameNation").WithForces(50).WithMoveRate(1).Build();
            var enemyBattalion = Battalion().WithNation("enemyNation").WithForces(50).WithMoveRate(1).Build();
            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, allyBattalion);
            map.Put(Vector2Int.up, enemyBattalion);
            var sut = CommandingOfficer().WithNation("sameNation").WithMap(map).Build();

            sut.AvailableTacticsAt(map.WhereIs(allyBattalion)!)
                .Should().NotContain(Tactic.Move);
        }

        [Test]
        public void Battalion_BecomesGuest_WhenMovingToSpaceOccupiedByJoinableBattalion()
        {
            var map = new Map(1, 2);
            var anAllyBattalion = Battalion().WithNation("sameNation").WithForces(100).WithMoveRate(1).Build();
            var damagedAllyBattalion = Battalion().WithNation("sameNation").WithForces(10).WithMoveRate(1).Build();
            map.Put(Vector2Int.zero, anAllyBattalion);
            map.Put(Vector2Int.up, damagedAllyBattalion);
            var itinerary = new List<Map.Space>
            {
                map.SpaceAt(new Vector2Int(0, 1)),
            };

            var sut = Maneuver.Move(anAllyBattalion, itinerary);
            sut.Apply(Situation().WithMap(map).Build());

            using var _ = new AssertionScope();
            map.SpaceAt(Vector2Int.zero).Occupant.Should().Be(Battalion.Null);
            map.WhereIs(anAllyBattalion).Should().Be(map.SpaceAt(new Vector2Int(0, 1)));
            map.SpaceAt(new Vector2Int(0, 1)).Guest.Should().Be(anAllyBattalion);
        }

        [Test]
        public void MergeTactic_Available_WhenBattalionsCanJoin()
        {
            var anAllyBattalion = Battalion().WithNation("sameNation").WithForces(100).WithMoveRate(1).Build();
            var damagedAllyBattalion = Battalion().WithNation("sameNation").WithForces(10).WithMoveRate(1).Build();
            var map = new Map(1, 1);
            map.Put(Vector2Int.zero, damagedAllyBattalion);
            map.SpaceAt(Vector2Int.zero).StopBy(anAllyBattalion);
            var sut = CommandingOfficer().WithNation("sameNation").WithMap(map).Build();

            sut.AvailableTacticsAt(map.WhereIs(anAllyBattalion)!)
                .Should().BeEquivalentTo(new List<Tactic> { Tactic.Merge });
        }

        [Test]
        public void MergeManeuver_WhenTotalPlatoonsLessThanMax()
        {
            var damagedAllyBattalion = Battalion().WithNation("sameNation").WithForces(3).WithMoveRate(1).Build();
            var anotherDamagedAllyBattalion = Battalion().WithNation("sameNation").WithForces(4).WithMoveRate(1).Build();
            var map = new Map(1, 1);
            map.Put(Vector2Int.zero, damagedAllyBattalion);
            map.SpaceAt(Vector2Int.zero).StopBy(anotherDamagedAllyBattalion);
            var treasury = new Treasury();
            var sut = Maneuver.Merge(anotherDamagedAllyBattalion, treasury);
            
            sut.Apply(Situation().WithMap(map).Build());

            using var _ = new AssertionScope();
            map.SpaceAt(Vector2Int.zero).Occupant.Forces.Should().Be(7);
            map.SpaceAt(Vector2Int.zero).Guest.Should().Be(Battalion.Null);
            treasury.WarFunds.Should().Be(0);
        }
        
        [Test]
        public void MergeManeuver_WhenPlatoonsOverflow()
        {
            var damagedAllyBattalion = Battalion().WithNation("sameNation").WithForces(95).WithPrice(1000).Build();
            var anotherDamagedAllyBattalion = Battalion().WithNation("sameNation").WithForces(90).WithPrice(1000).Build();
            var map = new Map(1, 1);
            map.Put(Vector2Int.zero, damagedAllyBattalion);
            map.SpaceAt(Vector2Int.zero).StopBy(anotherDamagedAllyBattalion);
            var treasury = new Treasury(0);
            var sut = Maneuver.Merge(anotherDamagedAllyBattalion, treasury);
            
            sut.Apply(Situation().WithMap(map).Build());

            using var _ = new AssertionScope();
            map.SpaceAt(Vector2Int.zero).Occupant.Forces.Should().Be(100);
            map.SpaceAt(Vector2Int.zero).Guest.Should().Be(Battalion.Null);
            treasury.WarFunds.Should().Be(800); //(Unit Price / 10) x (PlatoonsA + PlatoonsB - 10)
        }
        
    }
}