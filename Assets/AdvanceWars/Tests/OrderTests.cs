using System.Collections.Generic;
using AdvanceWars.Runtime;
using AdvanceWars.Tests.Builders;
using AdvanceWars.Tests.Doubles;
using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.BattalionBuilder;
using static AdvanceWars.Tests.Builders.TerrainBuilder;
using Battalion = AdvanceWars.Runtime.Battalion;

namespace AdvanceWars.Tests
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void CanNotPerform_anyManeuver_afterWait()
        {
            var sut = new CommandingOfficer();
            var battalion = Battalion().Build();

            sut.Order(Maneuver.Wait(battalion), map: default);

            sut.AvailableTacticsOf(battalion).Should().BeEmpty();
        }

        [Test]
        public void AfterFireManeuver_AutoWait()
        {
            var sut = new CommandingOfficer();
            var performer = Battalion().Build();

            sut.Order(FakeManeuver.Fire(performer), map: default);

            sut.AvailableTacticsOf(performer).Should().BeEmpty();
        }

        [Test]
        public void Troops_CanFire_AfterMove()
        {
            var sut = new CommandingOfficer();
            var battalion = Battalion().Build();

            sut.Order(FakeManeuver.Move(battalion), map: default);

            sut.AvailableTacticsOf(battalion).Should().Contain(Tactic.Fire);
        }

        [Test]
        public void ApplyFire_OnMap()
        {
            var anyArmor = new Armor("Any");
            var atk = Battalion()
                .Friend()
                .WithArmor("Any")
                .WithWeapon(WeaponBuilder.Weapon().WithDamage(anyArmor, 100).Build())
                .WithForces(200)
                .Build();

            var def = Battalion()
                .Enemy()
                .WithArmor("Any")
                .WithWeapon(WeaponBuilder.Weapon().WithDamage(anyArmor, 100).Build())
                .WithForces(300)
                .Build();

            var sut = Maneuver.Fire(atk, def);

            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, Terrain().Build());
            map.Put(Vector2Int.zero, atk);
            map.Put(Vector2Int.up, Terrain().Build());
            map.Put(Vector2Int.up, def);
            sut.Apply(map);

            using var _ = new AssertionScope();
            atk.Forces.Should().BeLessThan(200);
            def.Forces.Should().BeLessThan(300);
        }

        [Test]
        public void ApplyFire_VanisherAttacker()
        {
            var atk = Battalion()
                .Friend()
                .WithArmor("Other")
                .WithWeapon(WeaponBuilder.Weapon().MaxDmgTo("Any").Build())
                .WithForces(200)
                .Build();

            var def = Battalion()
                .Enemy()
                .WithArmor("Any")
                .WithWeapon(WeaponBuilder.Weapon().WithDamage(new Armor("Any"), 1).Build())
                .WithForces(300)
                .Build();

            var sut = Maneuver.Fire(atk, def);

            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, Terrain().Build());
            map.Put(Vector2Int.zero, atk);
            map.Put(Vector2Int.up, Terrain().Build());
            map.Put(Vector2Int.up, def);

            sut.Apply(map);

            using var _ = new AssertionScope();
            atk.Forces.Should().Be(200);
            def.Forces.Should().BeLessOrEqualTo(0);
            map.WhereIs(def).Should().BeNull();
        }

        [Test]
        public void ApplyFire_VanisherDefender()
        {
            var def = Battalion()
                .Friend()
                .WithArmor("Other")
                .WithWeapon(WeaponBuilder.Weapon().MaxDmgTo("Any").Build())
                .WithForces(200)
                .Build();

            var atk = Battalion()
                .Enemy()
                .WithArmor("Any")
                .WithWeapon(WeaponBuilder.Weapon().WithDamage(new Armor("Any"), 1).Build())
                .WithForces(300)
                .Build();

            var sut = Maneuver.Fire(atk, def);

            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, Terrain().Build());
            map.Put(Vector2Int.zero, atk);
            map.Put(Vector2Int.up, Terrain().Build());
            map.Put(Vector2Int.up, def);

            sut.Apply(map);

            using var _ = new AssertionScope();
            atk.Forces.Should().BeLessOrEqualTo(0);
            map.WhereIs(atk).Should().BeNull();
        }

        [Test]
        public void ApplyMovement_ForAdjacentSpace()
        {
            var map = new Map(1, 2);
            var battalion = Battalion().Build();
            map.Put(Vector2Int.zero, battalion);

            var itinerary = new List<Map.Space> { map.OfCoords(Vector2Int.up) };

            var sut = Maneuver.Move(battalion, itinerary);
            sut.Apply(map);

            using var _ = new AssertionScope();
            map.OfCoords(Vector2Int.zero).Occupant.Should().Be(Battalion.Null);
            map.WhereIs(battalion).Should().Be(map.OfCoords(Vector2Int.up));
        }

        [Test]
        public void ApplyMovement_ForMultipleSpace()
        {
            var map = new Map(1, 3);
            var battalion = Battalion().Build();
            map.Put(Vector2Int.zero, battalion);

            var itinerary = new List<Map.Space>
            {
                map.OfCoords(new Vector2Int(0, 1)),
                map.OfCoords(new Vector2Int(0, 2))
            };

            var sut = Maneuver.Move(battalion, itinerary);
            sut.Apply(map);

            using var _ = new AssertionScope();
            map.OfCoords(Vector2Int.zero).Occupant.Should().Be(Battalion.Null);
            map.WhereIs(battalion).Should().Be(map.OfCoords(new Vector2Int(0, 2)));
        }

        [Test]
        public void CommandingOfficer_OrdersManouvers()
        {
            var sut = new CommandingOfficer();
            var maneuverMock = Substitute.For<IManeuver>();

            sut.Order(maneuverMock, map: default);

            maneuverMock.ReceivedWithAnyArgs().Apply(default);
        }
    }
}