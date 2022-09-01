using AdvanceWars.Runtime;
using AdvanceWars.Tests.Builders;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.BattalionBuilder;
using static AdvanceWars.Tests.Builders.TerrainBuilder;
using static AdvanceWars.Tests.Builders.TheaterOpsBuilder;
using static AdvanceWars.Tests.Builders.WeaponBuilder;
using Battalion = AdvanceWars.Runtime.Battalion;

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
            Battalion().WithForces(forces).Build()
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
                    attacker: Battalion().WithForces(offForces).Build(),
                    defender: Battalion.Null
                )
                .Effectivity
                .Should().Be(expected);
        }

        [Test]
        public void DamageReduction_WhenTerrain_DoesntCover()
        {
            new Offensive
                (
                    attacker: Battalion.Null,
                    defender: Battalion().WithForces(100).Build()
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
                    attacker: Battalion.Null,
                    defender: Battalion().WithForces(forces).Build(),
                    battlefield: Terrain().WithDefense(defensiveRating).Build()
                )
                .DamageReductionMultiplier
                .Should().Be(expected);
        }

        [Test]
        public void BaseDamage_ObtainedFromWeapon()
        {
            var sut = Battalion()
                .WithWeapon
                    (Weapon().WithDamage(new Armor(), 10).Build())
                .Build();

            sut.BaseDamageTo(new Armor())
                .Should().Be(10);
        }

        [Test]
        public void FinalDamage_FromFormula()
        {
            var weapon = Weapon().WithDamage(new Armor(), 100).Build();
            var sut = new Offensive
            (
                attacker: Battalion().WithWeapon(weapon).WithForces(100).Build(),
                defender: Battalion().WithForces(50).Build(),
                battlefield: Terrain().WithDefense(1).Build()
            );
            sut.Damage.Should().Be(95);
        }

        [Test]
        public void AttackerVanishesTheDefender()
        {
            var weapon = Weapon().MaxDmgTo("SameArmor").Build();

            var sut = new Offensive
            (
                attacker: Battalion().WithWeapon(weapon).WithForces(100).Build(),
                defender: Battalion().WithForces(50).WithArmor("SameArmor").Build(),
                battlefield: Terrain().WithDefense(1).Build()
            );

            sut.Outcome().Should().Be(Battalion.Null);
        }

        [Test]
        public void AttackerDamagesDefender()
        {
            var weapon = Weapon().WithDamage(new Armor(), 100).Build();
            var defender = Battalion().Of(UnitBuilder.Unit()).WithForces(100);
            var sut = new Offensive
            (
                attacker: Battalion().WithWeapon(weapon).WithForces(100).Build(),
                defender: defender.Build(),
                battlefield: Terrain().WithDefense(1).Build()
            );
            sut.Outcome().Should().BeEquivalentTo(defender.WithForces(10).Build());
        }

        [Test]
        public void CombatHasOutcomesForAttackAndCounterAttack()
        {
            var atk = TheaterOps().Who
            (
                Battalion().WithWeapon
                (
                    Weapon().Build()
                ).Build()
            ).Build();
            var sut = new Combat(atk, def: TheaterOps().Build());

            var result = sut.PredictOutcome();

            result.Atk.Should().NotBeNull();
            result.Def.Should().NotBeNull();
        }

        [Test]
        public void CombatWithVanisherAttacker_HasNullDefenderOutcome()
        {
            var defending = TheaterOps().Build();
            var attacking = TheaterOps().WithVanisherOf(defending).Build();
            var sut = new Combat(attacking, defending);

            var result = sut.PredictOutcome();
            result.Atk.Should().NotBe(Battalion.Null);
            result.Def.Should().Be(Battalion.Null);
        }

        [Test]
        public void CombatWithNotVanisherAttacker_HasNotNullDefenderOutcome()
        {
            var sut = new Combat
            (
                atk: TheaterOps().Build(),
                def: TheaterOps().Who(Battalion().WithArmor("aGoodArmor!").Build()).Build()
            );

            var result = sut.PredictOutcome();

            result.Def.Should().NotBe(Battalion.Null);
        }

        [Test]
        public void CombatWithVanisherDefender_HasNullAttackerOutcome()
        {
            var atk = TheaterOps().Build();
            var sut = new Combat(atk, def: TheaterOps().WithVanisherOf(atk).Build());

            var result = sut.PredictOutcome();

            result.Atk.Should().Be(Battalion.Null);
        }

        [Test]
        public void CombatWithNotVanisherDefender_HasNotNullDefenderOutcome()
        {
            var sut = new Combat
            (
                atk: TheaterOps().Who(Battalion().WithArmor("aGoodArmor!").Build()).Build(),
                def: TheaterOps().Who(Battalion().WithArmor("alsoGoodArmor!").Build()).Build()
            );

            var result = sut.PredictOutcome();

            result.Atk.Should().NotBe(Battalion.Null);
        }

        [Test]
        public void NoBattalion_Offensive_ToBattalion()
        {
            var sut = new Offensive(Battalion.Null, Battalion().Build());
            sut.Outcome().Should().NotBeNull();
        }

        [Test]
        public void NoBattalion_Offensive_NoBattalion()
        {
            var sut = new Offensive(Battalion.Null, Battalion.Null);
            sut.Outcome().Should().BeEquivalentTo(Battalion.Null);
        }

        
        // O X O 
        // X B X 

        [Test]
        public void Map_RangeOfFire_WithRangeOne()
        {
            var sut = new Map(3, 2);
            var result = sut.RangeOfFire(new Vector2Int(1, 0), 1);
            result.Should().BeEquivalentTo(new Vector2Int[]
            {
                Vector2Int.zero, 
                new Vector2Int(2, 0),
                Vector2Int.one,  
            });
        }
        
        
        // O O X O O
        // O X X X O
        // X X B X X
        // O X X X O
        // O O X O O
        [Test]
        public void Map_RangeOfFire_WithRangeTwo()
        {
            var sut = new Map(5, 5);
            var result = sut.RangeOfFire(new Vector2Int(2, 2), 2);
            result.Should().BeEquivalentTo(new Vector2Int[]
            {
                new Vector2Int(2, 0),
                new Vector2Int(1, 1),
                new Vector2Int(2, 1),
                new Vector2Int(3, 1),
                new Vector2Int(0, 2),
                new Vector2Int(1, 2),
                new Vector2Int(3, 2),
                new Vector2Int(4, 2),
                new Vector2Int(1, 3),
                new Vector2Int(2, 3),
                new Vector2Int(3, 3),
                new Vector2Int(2, 4),
            });
        }

        [Test]
        public void Map_RangeOfFire_WithMinRangeTwo_andMaxRangeTwo()
        {
            var sut = new Map(1, 3);
            var result = sut.RangeOfFire(new Vector2Int(0, 0), 2, 2);
            result.Should().BeEquivalentTo(new Vector2Int[]
            {
                new Vector2Int(0, 2)
            });
        }
        
        [Test]
        public void RangeOfFireOfBattalion()
        {
            var sut = new Map(1, 3);
            var battalion = Battalion().WithRange(2, 2).Build();
            
            sut.Put(Vector2Int.zero, battalion);
            var result = sut.RangeOfFire(battalion);
            result.Should().BeEquivalentTo(new Vector2Int[]
            {
                new Vector2Int(0, 2)
            });
        }
    }
}