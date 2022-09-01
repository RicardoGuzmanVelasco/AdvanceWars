using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.BattalionBuilder;

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
    }
}