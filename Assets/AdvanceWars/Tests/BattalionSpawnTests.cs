using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Orders.Maneuvers;
using AdvanceWars.Runtime.Domain.Troops;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.BattalionBuilder;
using static AdvanceWars.Tests.Builders.CommandingOfficerBuilder;
using static AdvanceWars.Tests.Builders.SpawnerBuilder;
using static AdvanceWars.Tests.Builders.UnitBuilder;
using Unit = AdvanceWars.Runtime.Domain.Troops.Unit;

namespace AdvanceWars.Tests
{
    public class BattalionSpawnTests
    {
        private Nation SomeNation => new Nation("SomeNation");
        private Nation AnyNation => new Nation("AnyNation");

        [Test]
        public void Space_WithSpawner_CanSpawnUnits()
        {
            var spawner = Spawner().Build();
            
            var sut = new Map.Space {Terrain = spawner};
            
            sut.SpawnableUnits.Should().NotBeEmpty();
        }
        
        [Test]
        public void Space_WithoutSpawner_CanNotSpawnUnits()
        {
            var sut = new Map.Space();
            
            sut.SpawnableUnits.Should().BeEmpty();
        }

        [Test]
        public void Space_WithSpawner_SpawnsBattalion()
        {
            var spawner = Spawner().Build();
            var sut = new Map.Space {Terrain = spawner};
            
            sut.SpawnHere(Unit().Build());

            sut.IsOccupied.Should().BeTrue();
        }

        [Test]
        public void Airfield_OnlySpawns_UnitsFromTheAirForce()
        {
            var airfield = Airfield().Build();
            var sut = new Map.Space {Terrain = airfield};
            
            sut.SpawnableUnits.
                Should().AllSatisfy(x => x.ServiceBranch.Should().Be(Military.AirForce));
        }

        [Test]
        public void SpawnedBattalionAllegiance_IsTheSameAs_SpawnerAllegiance()
        {
            var spawner = Airfield().WithOwner(AnyNation).Build();
            var sut = new Map.Space {Terrain = spawner};

            sut.SpawnHere(Unit().Of(Military.AirForce).Build());

            sut.Occupant.IsAlly(spawner).Should().BeTrue();
        }

        [Test]
        public void Battalions_MayNotPerformAnyManeuver_WhenJustRecruited()
        {
            var spawner = Spawner().WithOwner(SomeNation).Build();
            var map = new Map(1,1);
            map.Put(Vector2Int.zero, spawner);
            var recruitManeuver = Maneuver.Recruit(spawner, Unit().Build(), new Treasury());
            var sut = CommandingOfficer().WithMap(map).WithNation(SomeNation).Build();
            
            sut.Order(recruitManeuver);

            sut.AvailableTacticsAt(map.SpaceAt(Vector2Int.zero)).Should().BeEmpty();
        }

        [Test]
        public void RecruitingUsesWarFunds()
        {
            var unit = Unit().With(1000);
            var spawner = Spawner().WithOwner(SomeNation).WithUnits(unit).Build();
            var map = new Map(1,1);
            map.Put(Vector2Int.zero, spawner);
            var sut = new Treasury(3000);
            var recruitManeuver = Maneuver.Recruit(spawner, unit.Build(), sut);
            var commandingOfficer = CommandingOfficer().WithMap(map).WithTreasury(sut).WithNation(SomeNation).Build();
            
            commandingOfficer.Order(recruitManeuver);

            sut.WarFunds.Should().Be(2000);
        }

        [Test]
        public void AvailableTacticsOf_Spawner_IsRecruit()
        {
            var spawner = Spawner().WithOwner(SomeNation).Build();
            var map = new Map(1,1);
            map.Put(Vector2Int.zero, spawner);
            var sut = CommandingOfficer().WithMap(map).WithNation(SomeNation).Build();
            
            sut.AvailableTacticsAt(map.WhereIs(spawner)!)
                .Should().BeEquivalentTo(new List<Tactic> { Tactic.Recruit});
        }
        
        [Test]
        public void RecruitTacticNotAvailable_WhenCanNotAfford()
        {
            var unit = Unit().With(1000);
            var spawner = Spawner().WithOwner(SomeNation).WithUnits(unit).Build();
            var map = new Map(1,1);
            map.Put(Vector2Int.zero, spawner);
            var treasury = new Treasury(999);
            var sut = CommandingOfficer().WithMap(map).WithTreasury(treasury).WithNation(SomeNation).Build();

            sut.AvailableTacticsAt(map.WhereIs(spawner))
                .Should().BeEmpty();
        }
        
        [Test]
        public void Spawner_AtOccupiedSpace_CannotRecruit()
        {
            var spawner = Spawner().WithOwner(SomeNation).Build();
            var map = new Map(1,1);
            map.Put(Vector2Int.zero, spawner);
            map.Put(Vector2Int.zero, Battalion().WithNation(SomeNation).Build());
            var sut = CommandingOfficer().WithMap(map).WithNation(SomeNation).Build();
            
            sut.AvailableTacticsAt(map.WhereIs(spawner)!)
                .Should().NotContain(Tactic.Recruit);
        }
    }
}