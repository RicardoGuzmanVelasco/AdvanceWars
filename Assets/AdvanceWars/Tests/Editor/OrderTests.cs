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
using static AdvanceWars.Tests.Builders.MapBuilder;
using static AdvanceWars.Tests.Builders.SituationBuilder;
using static AdvanceWars.Tests.Builders.TerrainBuilder;
using static AdvanceWars.Tests.Builders.WeaponBuilder;
using Battalion = AdvanceWars.Runtime.Domain.Troops.Battalion;

namespace AdvanceWars.Tests
{
    [TestFixture]
    public class OrderTests
    {
        private int AnyForces => 80;
        private int MinDamage => 80;

        [Test]
        public void CanNotPerform_anyManeuver_afterWait()
        {
            var map = new Map(1, 1);
            var sut = CommandingOfficer().WithMap(map).WithNation("aNation").Build();
            var battalion = Battalion().WithNation("aNation").Build();
            map.Put(Vector2Int.zero, battalion);

            sut.Order(Maneuver.Wait(battalion));

            sut.AvailableTacticsAt(map.WhereIs(battalion)!).Should().BeEmpty();
        }

        [Test]
        public void AfterFireManeuver_AutoWait()
        {
            var map = new Map(1, 1);
            var sut = CommandingOfficer().WithMap(map).WithNation("aNation").Build();
            var battalion = Battalion().WithNation("aNation").Build();
            map.Put(Vector2Int.zero, battalion);

            sut.Order(new DummyManeuver(Tactic.Fire, battalion));

            sut.AvailableTacticsAt(map.WhereIs(battalion)!).Should().BeEmpty();
        }

        [Test]
        public void Troops_MayUseAnyOtherTactic_AfterMove()
        {
            var map = new Map(1, 2);
            var ally = Battalion()
                .WithNation("Ally")
                .WithPrimaryWeapon(Weapon().WithDamage(new Armor("EnemyArmor"), 1).Build())
                .Build();
            var enemy = Battalion().WithNation("Enemy").WithArmor("EnemyArmor").Build();
            var sut = CommandingOfficer().WithNation("Ally").WithMap(map).Build();

            map.Put(Vector2Int.zero, ally);
            map.Put(Vector2Int.up, enemy);

            sut.Order(new DummyManeuver(Tactic.Move, ally));

            sut.AvailableTacticsAt(map.WhereIs(ally)!).Should().Contain(Tactic.Fire);
        }

        [Test]
        public void ApplyFire_OnMap()
        {
            var anyArmor = new Armor("Any");
            var atk = Battalion()
                .Ally()
                .WithArmor("Any")
                .WithPrimaryWeapon(Weapon().WithDamage(anyArmor, MinDamage).Build())
                .WithForces(AnyForces)
                .Build();

            var def = Battalion()
                .Enemy()
                .WithArmor("Any")
                .WithPrimaryWeapon(Weapon().WithDamage(anyArmor, MinDamage).Build())
                .WithForces(AnyForces)
                .Build();

            var sut = Maneuver.Fire(atk, def);

            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, Terrain().Build());
            map.Put(Vector2Int.zero, atk);
            map.Put(Vector2Int.up, Terrain().Build());
            map.Put(Vector2Int.up, def);
            sut.Apply(Situation().WithMap(map).Build());

            using var _ = new AssertionScope();
            atk.Forces.Should().BeLessThan(AnyForces);
            def.Forces.Should().BeLessThan(AnyForces);
        }

        [Test]
        public void ApplyFire_VanisherAttacker()
        {
            var atk = Battalion()
                .Ally()
                .WithArmor("Other")
                .WithPrimaryWeapon(Weapon().MaxDmgTo("Any").Build())
                .WithForces(AnyForces)
                .Build();

            var def = Battalion()
                .Enemy()
                .WithArmor("Any")
                .WithPrimaryWeapon(Weapon().WithDamage(new Armor("Any"), 1).Build())
                .WithForces(AnyForces)
                .Build();

            var sut = Maneuver.Fire(atk, def);

            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, Terrain().Build());
            map.Put(Vector2Int.zero, atk);
            map.Put(Vector2Int.up, Terrain().Build());
            map.Put(Vector2Int.up, def);

            sut.Apply(Situation().WithMap(map).Build());

            using var _ = new AssertionScope();
            atk.Forces.Should().Be(AnyForces);
            def.Forces.Should().Be(0);
            map.WhereIs(def).Should().BeNull();
        }

        [Test]
        public void ApplyFire_VanisherDefender()
        {
            var def = Battalion()
                .Ally()
                .WithArmor("Other")
                .WithPrimaryWeapon(Weapon().MaxDmgTo("Any").Build())
                .WithForces(AnyForces)
                .Build();

            var atk = Battalion()
                .Enemy()
                .WithArmor("Any")
                .WithPrimaryWeapon(Weapon().WithDamage(new Armor("Any"), 1).Build())
                .WithForces(AnyForces)
                .Build();

            var sut = Maneuver.Fire(atk, def);

            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, Terrain().Build());
            map.Put(Vector2Int.zero, atk);
            map.Put(Vector2Int.up, Terrain().Build());
            map.Put(Vector2Int.up, def);

            sut.Apply(Situation().WithMap(map).Build());

            using var _ = new AssertionScope();
            atk.Forces.Should().Be(0);
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
            sut.Apply(Situation().WithMap(map).Build());

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
            sut.Apply(Situation().WithMap(map).Build());

            using var _ = new AssertionScope();
            map.SpaceAt(Vector2Int.zero).Occupant.Should().Be(Battalion.Null);
            map.WhereIs(battalion).Should().Be(map.SpaceAt(new Vector2Int(0, 2)));
        }

        [Test]
        public void CommandingOfficer_OrdersManeuvers()
        {
            var sut = CommandingOfficer().WithNation("aNation").Build();
            var performer = Battalion().WithNation("aNation").Build();

            var maneuverMock = Substitute.For<IManeuver>();
            maneuverMock.Performer.Returns(performer);

            sut.Order(maneuverMock);

            maneuverMock.ReceivedWithAnyArgs().Apply(default);
        }

        [Test]
        public void AfterBeginTurn_AllTacticsAreAvailableAgain()
        {
            var map = new Map(1, 1);
            var sut = CommandingOfficer().WithMap(map).WithNation("aNation").Build();
            var battalion = Battalion().WithNation("aNation").Build();
            map.Put(Vector2Int.zero, battalion);

            sut.Order(Maneuver.Wait(battalion));
            sut.BeginTurn();

            sut.AvailableTacticsAt(map.WhereIs(battalion)!).Should().NotBeEmpty();
        }

        [Test]
        public void MoveTactic_IsNotAvailable_WhenNoSpacesAvailable()
        {
            var map = new Map(1, 1);
            var battalion = Battalion().WithNation("aNation").Build();
            map.Put(Vector2Int.zero, battalion);
            var sut = CommandingOfficer().WithNation("aNation").WithMap(map).Build();

            sut.AvailableTacticsAt(map.WhereIs(battalion)!).Should().NotContain(Tactic.Move);
        }

        [Test]
        public void FireManeuver_ConsumesOneAmmoRound_BothForPerformerAndTarget()
        {
            var map = Map().Of(1, 2).Build();

            var performer = Battalion().Ally().WithAmmo(10).Build();
            map.Put(Vector2Int.zero, performer);
            map.Put(Vector2Int.zero, Terrain().Build());

            var target = Battalion().Enemy().WithAmmo(5).Build();
            map.Put(Vector2Int.up, target);
            map.Put(Vector2Int.up, Terrain().Build());

            var sut = Maneuver.Fire(performer, target);

            sut.Apply(Situation().WithMap(map).Build());

            performer.AmmoRounds.Should().Be(9);
            target.AmmoRounds.Should().Be(4);
        }

        [Test]
        public void FireTactic_IsNotAvailable_WhenAttackerHasNoAmmoRoundsLeft()
        {
            var map = Map().Of(1, 2).Build();

            var aNation = "aNation";
            var ally = Battalion()
                .WithNation(aNation)
                .WithPrimaryWeapon(Weapon().WithDamage(new Armor("EnemyArmor"), 1).Build())
                .WithAmmo(0)
                .Build();
            map.Put(Vector2Int.zero, ally);

            var enemy = Battalion().WithNation("Enemy").WithArmor("EnemyArmor").Build();
            map.Put(Vector2Int.up, enemy);

            var sut = CommandingOfficer().WithNation(aNation).WithMap(map).Build();

            sut.AvailableTacticsAt(map.WhereIs(ally)!).Should().NotContain(Tactic.Fire);
        }
    }
}