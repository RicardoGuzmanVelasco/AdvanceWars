using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.BatallionBuilder;
using static AdvanceWars.Tests.Builders.TerrainBuilder;
using static AdvanceWars.Tests.Builders.UnitBuilder;
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
                .BaseDamageTo(Unit().Build())
                .Should().Be(0);
        }

        [Test]
        public void AssignedBaseDamage_IsGreaterThanZero()
        {
            var theSameUnit = Unit().Build();
            Weapon().WithDamage(theSameUnit, 10).Build()
                .BaseDamageTo(theSameUnit)
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
            var theSameUnit = Unit().Build();
            var sut = Batallion()
                .WithWeapon
                    (Weapon().WithDamage(theSameUnit, 10).Build())
                .Build();

            sut.BaseDamageTo(theSameUnit)
                .Should().Be(10);
        }

        [Test]
        public void FinalDamage_FromFormula()
        {
            var weapon = Weapon().WithDamage(Batallion().Build().Unit, 100).Build();
            var sut = new Offensive(
                attacker: Batallion().WithWeapon(weapon).WithForces(100).Build(),
                defender: Batallion().WithForces(50).Build(),
                battlefield: Terrain().WithDefense(1).Build()
            );
            sut.Damage().Should().Be(95);
        }
    }
}