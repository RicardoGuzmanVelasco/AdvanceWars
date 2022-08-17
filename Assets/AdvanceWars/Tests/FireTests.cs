using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.BatallionBuilder;
using static AdvanceWars.Tests.TerrainBuilder;
using static AdvanceWars.Tests.WeaponBuilder;
using Batallion = AdvanceWars.Runtime.Batallion;

namespace AdvanceWars.Tests
{
    public class FireTests
    {
        [Test]
        public void Weapon_DoesNoDamage_ByDefault()
        {
            Weapon().Build()
                .BaseDamageTo(new Unit())
                .Should().Be(0);
        }

        [Test]
        public void AssignedBaseDamage_IsGreaterThanZero()
        {
            var sut = Weapon().WithDamage(new Unit(), 10).Build();

            sut.BaseDamageTo(new Unit())
                .Should().Be(10);
        }

        [TestCase(40, 4)]
        [TestCase(48, 4)]
        [TestCase(9, 1)]
        public void Platoons_Derives_FromForces(int forces, int effectivity)
        {
            var sut = Batallion().WithForces(forces).Build();
            sut.Platoons.Should().Be(effectivity);
        }

        [TestCase(100, 1)]
        [TestCase(95, .9f)]
        [TestCase(9, .1f)]
        public void OffensiveEffectivity_Derives_FromForces(int offForces, float expected)
        {
            var sut = new Offensive
            (
                attacker: Batallion().WithForces(offForces).Build(),
                defender: Batallion.Null
            );
            sut.Effectivity.Should().Be(expected);
        }

        [Test]
        public void DamageReduction_WhenTerrain_DoesntCover()
        {
            var sut = new Offensive
            (
                attacker: Batallion.Null,
                defender: Batallion().WithForces(100).Build()
            );
            sut.DamageReductionMultiplier.Should().Be(1);
        }

        [TestCase(1, 100, 0.9f)]
        [TestCase(1, 50, 0.95f)]
        public void DamageReduction_WhenTerrain_Cover(int defensiveRating, int forces, float expected)
        {
            var sut = new Offensive
            (
                attacker: Batallion.Null,
                defender: Batallion().WithForces(forces).Build(),
                battlefield: Terrain().WithDefense(defensiveRating).Build()
            );
            sut.DamageReductionMultiplier.Should().Be(expected);
        }

        [Test]
        public void BaseDamage_ObtainedFromWeapon()
        {
            var sut = Batallion()
                .WithWeapon
                    (Weapon().WithDamage(new Unit(), 10).Build())
                .Build();

            sut.BaseDamageTo(new Unit())
                .Should().Be(10);
        }

        [Test]
        public void METHOD()
        {
            var weapon = Weapon().WithDamage(Batallion().Build().Unit, 100).Build();
            var unit = UnitBuilder.Unit()...;
            var sut = new Offensive(
                attacker: Batallion().WithWeapon(weapon).WithForces(100).Build(),
                defender: Batallion().WithForces(50).Build(),
                battlefield: Terrain().WithDefense(1).Build()
            );
            sut.Damage().Should().Be(95);
        }
    }
}