using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.BattalionBuilder;
using static AdvanceWars.Tests.Builders.CommandingOfficerBuilder;

namespace AdvanceWars.Tests
{
    public class RangeOfFireTests
    {
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
        
        [Test]
        public void BattalionsInRange()
        {
            var sut = new Map(1, 2);
            var ally = Battalion().WithNation("Ally").WithRange(1, 1).Build();
            var enemy = Battalion().WithNation("Enemy").Build();
            
            sut.Put(Vector2Int.zero, ally);
            sut.Put(Vector2Int.up, enemy);
            var result = sut.EnemyBattalionsInRangeOfFire(ally);
            
            result.Should().Contain(new Battalion[]
            {
                enemy
            });
        }
        
        [Test]
        public void Fire_WhenInsideRange_IsAnAvailableTactic()
        {
            var allyBattalion = Battalion().WithNation("ally").WithPlatoons(1).WithRange(1, 1).Build();
            var enemyBattalion = Battalion().WithNation("enemy").WithPlatoons(1).Build();

            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, allyBattalion);
            map.Put(Vector2Int.up, enemyBattalion);

            var sut = CommandingOfficer().WithNation("ally").WithMap(map).Build();

            sut.AvailableTacticsOf(allyBattalion).Should().Contain(Tactic.Fire);
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
        
            sut.AvailableTacticsOf(allyBattalion).Should().NotContain(Tactic.Fire);
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
        
            sut.AvailableTacticsOf(allyBattalion).Should().NotContain(Tactic.Fire);
        }
    }
}