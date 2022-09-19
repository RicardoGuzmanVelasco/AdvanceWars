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
        public void MoveTactic_AvailableTo_JoinableBattalions()
        {
            var anAllyBattallion = Battalion().WithNation("sameNation").WithForces(100).Build();
            var anotherAllyBattallion = Battalion().WithNation("sameNation").WithForces(10).Build();
            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, anAllyBattallion);
            map.Put(Vector2Int.up, anotherAllyBattallion);
            var sut = CommandingOfficer().WithNation("sameNation").WithMap(map).Build();

            sut.AvailableTacticsOf(anAllyBattallion)
                .Should().Contain(Tactic.Move);
        }
    }
}