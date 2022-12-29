using System.Collections.Generic;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Orders.Maneuvers;
using AdvanceWars.Tests.Builders;
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
        BattalionBuilder FreshBattalion => Battalion().WithForces(100).WithMoveRate(1);
        BattalionBuilder ADamagedBattalion => Battalion().WithForces(40).WithMoveRate(1);

        [Test]
        public void MergeTactic_NotAvailableForEnemies()
        {
            var invalidDonor = ADamagedBattalion.Ally().Build();
            var recipient = ADamagedBattalion.Enemy().Build();
            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, invalidDonor);
            map.Put(Vector2Int.up, recipient);
            var sut = CommandingOfficer().Ally().WithMap(map).Build();

            sut.AvailableTacticsAt(map.WhereIs(invalidDonor)!)
                .Should().NotContain(Tactic.Merge);
        }

        [Test]
        public void MergeTactic_Available_WhenBattalionsCanJoin()
        {
            var donor = FreshBattalion.Ally().Build();
            var recipient = ADamagedBattalion.Ally().Build();
            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, donor);
            map.Put(Vector2Int.up, recipient);
            var sut = CommandingOfficer().Ally().WithMap(map).Build();

            sut.AvailableTacticsAt(map.WhereIs(donor)!)
                .Should().BeEquivalentTo(new List<Tactic> { Tactic.Merge });
        }

        [Test]
        public void MergeManeuver_WhenTotalPlatoonsLessThanMax()
        {
            var donor = Battalion().Ally().WithForces(3).WithMoveRate(1).Build();
            var recipient =
                Battalion().Ally().WithForces(4).WithMoveRate(1).Build();
            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, donor);
            map.Put(Vector2Int.up, recipient);
            var treasury = new Treasury();
            var sut = Maneuver.Merge(donor, recipient, treasury);

            sut.Apply(Situation().WithMap(map).Build());

            using var _ = new AssertionScope();
            map.SpaceAt(Vector2Int.up).Occupant.Forces.Value.Should().Be(7);
            map.SpaceAt(Vector2Int.zero).Occupant.Should().Be(Battalion.Null);
            treasury.WarFunds.Should().Be(0);
        }

        [Test]
        public void MergeManeuver_WhenForcesOverflow()
        {
            var donor = Battalion().Ally().WithForces(95).WithPrice(1000).Build();
            var recipient =
                Battalion().Ally().WithForces(90).WithPrice(1000).Build();
            var map = new Map(1, 1);
            map.Put(Vector2Int.zero, donor);
            map.Put(Vector2Int.up, recipient);
            var treasury = new Treasury();
            var sut = Maneuver.Merge(donor, recipient, treasury);

            sut.Apply(Situation().WithMap(map).Build());

            using var _ = new AssertionScope();
            map.SpaceAt(Vector2Int.up).Occupant.Forces.Value.Should().Be(Battalion.MaxForces);
            map.SpaceAt(Vector2Int.zero).Occupant.Should().Be(Battalion.Null);
            treasury.WarFunds.Should().Be(850); //Price Per Soldier x (ForcesA + ForcesB - MaxForces)
        }
    }
}