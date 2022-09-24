using System.Collections.Generic;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Orders.Maneuvers;
using AdvanceWars.Runtime.Domain.Troops;
using AdvanceWars.Tests.Doubles;
using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.BattalionBuilder;
using static AdvanceWars.Tests.Builders.CommandingOfficerBuilder;
using static AdvanceWars.Tests.Builders.TerrainBuilder;
using static AdvanceWars.Tests.Builders.WeaponBuilder;
using Battalion = AdvanceWars.Runtime.Domain.Troops.Battalion;

namespace AdvanceWars.Tests
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void CanNotPerform_anyManeuver_afterWait()
        {
            var sut = CommandingOfficer().WithNation("aNation").Build();
            var battalion = Battalion().WithNation("aNation").Build();

            sut.Order(Maneuver.Wait(battalion));

            sut.AvailableTacticsOf(battalion).Should().BeEmpty();
        }

        [Test]
        public void AfterFireManeuver_AutoWait()
        {
            var sut = CommandingOfficer().WithNation("aNation").Build();
            var troop = Battalion().WithNation("aNation").Build();

            sut.Order(new DummyManeuver(Tactic.Fire, troop));

            sut.AvailableTacticsOf(troop).Should().BeEmpty();
        }

        [Test]
        public void Troops_MayUseAnyOtherTactic_AfterMove()
        {
            var map = new Map(1, 2);
            var ally = Battalion()
                .WithNation("Ally")
                .WithWeapon(Weapon().WithDamage(new Armor("EnemyArmor"), 1).Build())
                .Build();
            var enemy = Battalion().WithNation("Enemy").WithArmor("EnemyArmor").Build();
            var sut = CommandingOfficer().WithNation("Ally").WithMap(map).Build();

            map.Put(Vector2Int.zero, ally);
            map.Put(Vector2Int.up, enemy);

            sut.Order(new DummyManeuver(Tactic.Move, ally));

            sut.AvailableTacticsOf(ally).Should().Contain(Tactic.Fire);
        }

        [Test]
        public void ApplyFire_OnMap()
        {
            var anyArmor = new Armor("Any");
            var atk = Battalion()
                .Ally()
                .WithArmor("Any")
                .WithWeapon(Weapon().WithDamage(anyArmor, 100).Build())
                .WithForces(200)
                .Build();

            var def = Battalion()
                .Enemy()
                .WithArmor("Any")
                .WithWeapon(Weapon().WithDamage(anyArmor, 100).Build())
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
                .Ally()
                .WithArmor("Other")
                .WithWeapon(Weapon().MaxDmgTo("Any").Build())
                .WithForces(200)
                .Build();

            var def = Battalion()
                .Enemy()
                .WithArmor("Any")
                .WithWeapon(Weapon().WithDamage(new Armor("Any"), 1).Build())
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
                .Ally()
                .WithArmor("Other")
                .WithWeapon(Weapon().MaxDmgTo("Any").Build())
                .WithForces(200)
                .Build();

            var atk = Battalion()
                .Enemy()
                .WithArmor("Any")
                .WithWeapon(Weapon().WithDamage(new Armor("Any"), 1).Build())
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

            var itinerary = new List<Map.Space> { map.SpaceAt(Vector2Int.up) };

            var sut = Maneuver.Move(battalion, itinerary);
            sut.Apply(map);

            using var _ = new AssertionScope();
            map.SpaceAt(Vector2Int.zero).Occupant.Should().Be(Battalion.Null);
            map.WhereIs(battalion).Should().Be(map.SpaceAt(Vector2Int.up));
        }

        [Test]
        public void ApplyMovement_ForMultipleSpace()
        {
            var map = new Map(1, 3);
            var battalion = Battalion().Build();
            map.Put(Vector2Int.zero, battalion);

            var itinerary = new List<Map.Space>
            {
                map.SpaceAt(new Vector2Int(0, 1)),
                map.SpaceAt(new Vector2Int(0, 2))
            };

            var sut = Maneuver.Move(battalion, itinerary);
            sut.Apply(map);

            using var _ = new AssertionScope();
            map.SpaceAt(Vector2Int.zero).Occupant.Should().Be(Battalion.Null);
            map.WhereIs(battalion).Should().Be(map.SpaceAt(new Vector2Int(0, 2)));
        }

        [Test]
        public void CommandingOfficer_OrdersManeuvers()
        {
            var sut = CommandingOfficer().WithNation("aNation").Build();
            var battalion = Battalion().WithNation("aNation").Build();

            var maneuverMock = Substitute.For<IManeuver>();
            maneuverMock.Performer.Returns(battalion);

            sut.Order(maneuverMock);

            maneuverMock.ReceivedWithAnyArgs().Apply(default);
        }

        [Test]
        public void AfterBeginTurn_AllTacticsAreAvailableAgain()
        {
            var sut = CommandingOfficer().WithNation("aNation").Build();
            var battalion = Battalion().WithNation("aNation").Build();

            sut.Order(Maneuver.Wait(battalion));
            sut.BeginTurn();

            sut.AvailableTacticsOf(battalion).Should().NotBeEmpty();
        }

        [Test]
        public void MoveTactic_isNotAvailable_WhenNoSpacesAvailable()
        {
            var map = new Map(1, 1);
            var battalion = Battalion().WithNation("aNation").Build();
            map.Put(Vector2Int.zero, battalion);
            var sut = CommandingOfficer().WithNation("aNation").WithMap(map).Build();

            sut.AvailableTacticsOf(battalion).Should().NotContain(Tactic.Move);
        }
    }
}