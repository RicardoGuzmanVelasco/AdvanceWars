using AdvanceWars.Runtime;
using AdvanceWars.Tests.Builders;
using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.BatallionBuilder;
using static AdvanceWars.Tests.Builders.TerrainBuilder;
using static AdvanceWars.Tests.Builders.WeaponBuilder;
using Batallion = AdvanceWars.Runtime.Batallion;

namespace AdvanceWars.Tests
{
    public class FireTests
    {
        [Test]
        public void Weapon_DoesNoDamage_ByDefault()
        {
            Weapon().Build()
                .BaseDamageTo(new Armor())
                .Should().Be(0);
        }

        [Test]
        public void AssignedBaseDamage_IsGreaterThanZero()
        {
            var theSameArmor = new Armor();
            Weapon().WithDamage(theSameArmor, 10).Build()
                .BaseDamageTo(theSameArmor)
                .Should().Be(10);
        }

        [TestCase(40, 4)]
        [TestCase(48, 4)]
        [TestCase(9, 1)]
        public void Platoons_Derives_FromForces(int forces, int effectivity)
        {
            Batallion().WithForces(forces).Build()
                .Platoons
                .Should().Be(effectivity);
        }

        [TestCase(100, 1)]
        [TestCase(95, .9f)]
        [TestCase(9, .1f)]
        public void OffensiveEffectivity_Derives_FromForces(int offForces, float expected)
        {
            new Offensive
                (
                    attacker: Batallion().WithForces(offForces).Build(),
                    defender: Batallion.Null
                )
                .Effectivity
                .Should().Be(expected);
        }

        [Test]
        public void DamageReduction_WhenTerrain_DoesntCover()
        {
            new Offensive
                (
                    attacker: Batallion.Null,
                    defender: Batallion().WithForces(100).Build()
                )
                .DamageReductionMultiplier
                .Should().Be(1);
        }

        [TestCase(1, 100, 0.9f)]
        [TestCase(1, 50, 0.95f)]
        public void DamageReduction_WhenTerrain_Cover(int defensiveRating, int forces, float expected)
        {
            new Offensive
                (
                    attacker: Batallion.Null,
                    defender: Batallion().WithForces(forces).Build(),
                    battlefield: Terrain().WithDefense(defensiveRating).Build()
                )
                .DamageReductionMultiplier
                .Should().Be(expected);
        }

        [Test]
        public void BaseDamage_ObtainedFromWeapon()
        {
            var theSameArmor = new Armor();
            var sut = Batallion()
                .WithWeapon
                    (Weapon().WithDamage(theSameArmor, 10).Build())
                .Build();

            sut.BaseDamageTo(theSameArmor)
                .Should().Be(10);
        }

        [Test]
        public void FinalDamage_FromFormula()
        {
            var weapon = Weapon().WithDamage(new Armor(), 100).Build();
            var sut = new Offensive
            (
                attacker: Batallion().WithWeapon(weapon).WithForces(100).Build(),
                defender: Batallion().WithForces(50).Build(),
                battlefield: Terrain().WithDefense(1).Build()
            );
            sut.Damage.Should().Be(95);
        }

        [Test]
        public void AttackerVanishesTheDefender()
        {
            var weapon = Weapon().MaxDmgTo(new Armor()).Build();

            var sut = new Offensive
            (
                attacker: Batallion().WithWeapon(weapon).WithForces(100).Build(),
                defender: Batallion().WithForces(50).Build(),
                battlefield: Terrain().WithDefense(1).Build()
            );

            sut.Outcome().Should().BeEquivalentTo(Batallion.Null);
        }

        [Test]
        public void AttackerDamagesDefender()
        {
            var weapon = Weapon().WithDamage(new Armor(), 100).Build();
            var defender = Batallion().Of(UnitBuilder.Unit().Build()).WithForces(100);

            var sut = new Offensive
            (
                attacker: Batallion().WithWeapon(weapon).WithForces(100).Build(),
                defender: defender.Build(),
                battlefield: Terrain().WithDefense(1).Build()
            );
            sut.Outcome().Should().BeEquivalentTo(defender.WithForces(10).Build());
        }

        [Test]
        public void CombatHasOutcomesForAttackAndCounterAttack()
        {
            var attacking = new TheaterOps(
                battlefield: Terrain().Build(),
                troop: Batallion().WithWeapon(Weapon().Build()).Build());
            var defending = new TheaterOps(battlefield: Terrain().Build(), troop: Batallion().Build());
            var sut = new Combat(attacking, defending);

            var result = sut.PredictOutcome();

            result.Attacker.Should().NotBeNull();
            result.Defender.Should().NotBeNull();
        }

        [Test]
        public void CombatWithAttackerVanish_HasNullDefenderOutcome()
        {
            var attacking = new TheaterOps(
                battlefield: Terrain().Build(),
                troop: Batallion().WithWeapon(Weapon().MaxDmgTo(new Armor()).Build()).Build());

            var defending = new TheaterOps(
                battlefield: Terrain().Build(),
                troop: Batallion().Build());

            var sut = new Combat(attacking, defending);

            var result = sut.PredictOutcome();

            result.Attacker.Should().NotBeEquivalentTo(Batallion.Null);
            result.Defender.Should().BeEquivalentTo(Batallion.Null);
        }
    }
}