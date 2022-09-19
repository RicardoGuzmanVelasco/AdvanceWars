using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.BattalionBuilder;
using static AdvanceWars.Tests.Builders.CommandingOfficerBuilder;

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

            sut.AvailableTacticsOf(fullHealthAllyBattalion)
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

            sut.AvailableTacticsOf(damagedAllyBattalion)
                .Should().NotContain(Tactic.Move);
        }
    }
}