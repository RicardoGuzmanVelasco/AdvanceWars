using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.BattalionBuilder;
using static AdvanceWars.Tests.Builders.CommandingOfficerBuilder;
using static AdvanceWars.Tests.Builders.RangeOfBuilder;
using static AdvanceWars.Tests.Builders.WeaponBuilder;
using Weapon = AdvanceWars.Runtime.Weapon;

namespace AdvanceWars.Tests
{
    public class RangeOfFireTests
    {
        [Test]
        public void Map_RangeOfFire_WithRangeOne()
        {
            var sut = new Map(3, 2);
            
            var result = sut.RangeOfFire(new Vector2Int(1, 0), 1);
            
            result.Should().BeEquivalentTo(RangeOf().WithStructure
            (
                @" O X O 
                   X B X"
            ).Build());
        }
       
        [Test]
        public void Map_RangeOfFire_WithRangeTwo()
        {
            var sut = new Map(5, 5);
            
            var result = sut.RangeOfFire(new Vector2Int(2, 2), 2);
            
            result.Should().BeEquivalentTo(RangeOf().WithStructure
                (
                    @" O O X O O 
                       O X X X O
                       X X B X X
                       O X X X O
                       O O X O O"
                ).Build());
        }

        [Test]
        public void Map_RangeOfFire_WithMinRangeTwo_andMaxRangeTwo()
        {
            var sut = new Map(1, 3);
            
            var result = sut.RangeOfFire(new Vector2Int(0, 0), new RangeOfFire(2, 2));
            
            result.Should().BeEquivalentTo(RangeOf().WithStructure
                (
                    @" X
                       O
                       B"
                ).Build());
        }
        
        [Test]
        public void RangeOfFireOfBattalion()
        {
            var sut = new Map(1, 3);
            var battalion = Battalion().WithRange(2, 2).Build();
            
            sut.Put(Vector2Int.zero, battalion);
            
            var result = sut.RangeOfFire(battalion);
            
            result.Should().BeEquivalentTo(RangeOf().WithStructure
                (
                    @" X
                       O
                       B"
                ).Build());
        }
        
        [Test]
        public void EnemyBattalionInRange_AppearsAsSuch()
        {
            var sut = new Map(1, 2);
            var ally = Battalion().WithNation("Ally").WithRange(1, 1).Build();
            var enemy = Battalion().WithNation("Enemy").Build();
            
            sut.Put(Vector2Int.zero, ally);
            sut.Put(Vector2Int.up, enemy);
            
            var result = sut.EnemyBattalionsInRangeOfFire(ally);
            
            result.Should().Contain(new Battalion[] { enemy });
        }
        
        [Test]
        public void Fire_WhenInsideRange_IsAnAvailableTactic()
        {
            var allyBattalion = Battalion()
                .WithNation("ally")
                .WithPlatoons(1)
                .WithRange(1, 1)
                .WithWeapon(Weapon().WithDamage(new Armor("EnemyArmor"), 1).Build())
                .Build();
            var enemyBattalion = Battalion().WithNation("enemy").WithArmor("EnemyArmor").WithPlatoons(1).Build();

            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, allyBattalion);
            map.Put(Vector2Int.up, enemyBattalion);

            var sut = CommandingOfficer().WithNation("ally").WithMap(map).Build();

            sut.AvailableTacticsOf(allyBattalion)
                .Should().Contain(Tactic.Fire);
        }
        
        [Test]
        public void Fire_WhenOutsideRange_IsNotAnAvailableTactic()
        {
            var allyBattalion = Battalion().WithNation("ally").WithPlatoons(1).WithRange(1, 1).Build();
            var enemyBattalion = Battalion().WithNation("enemy").WithPlatoons(1).Build();
        
            var map = new Map(1, 3);
            map.Put(Vector2Int.zero, allyBattalion);
            map.Put(new Vector2Int(0, 2), enemyBattalion);
        
            var sut = CommandingOfficer().WithNation("ally").WithMap(map).Build();
        
            sut.AvailableTacticsOf(allyBattalion)
                .Should().NotContain(Tactic.Fire);
        }
        
        [Test]
        public void Fire_AgainstAnAlly_IsNotAnAvailableTactic()
        {
            var allyBattalion = Battalion().WithNation("ally").WithPlatoons(1).WithRange(1, 1).Build();
            var enemyBattalion = Battalion().WithNation("ally").WithPlatoons(1).Build();
        
            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, allyBattalion);
            map.Put(Vector2Int.up, enemyBattalion);
        
            var sut = CommandingOfficer().WithNation("ally").WithMap(map).Build();
        
            sut.AvailableTacticsOf(allyBattalion)
                .Should().NotContain(Tactic.Fire);
        }

        [Test]
        public void CanNotFire_WhenDoesNotHaveAWeapon()
        {
            var allyBattalion = Battalion().WithNation("ally").WithWeapon(Weapon.Null).WithPlatoons(1).WithRange(1, 1).Build();
            var enemyBattalion = Battalion().WithNation("enemy").WithPlatoons(1).Build();
        
            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, allyBattalion);
            map.Put(Vector2Int.up, enemyBattalion);
        
            var sut = CommandingOfficer().WithNation("ally").WithMap(map).Build();
        
            sut.AvailableTacticsOf(allyBattalion)
                .Should().NotContain(Tactic.Fire);
        }
    }
}