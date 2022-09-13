using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.MapBuilder;
using static AdvanceWars.Tests.Builders.RangeOfBuilder;

namespace AdvanceWars.Tests
{
    public class RangeOfBuilderTests
    {
        [Test]
        public void RangeOfFromString_WithSingleSpace()
        {
            RangeOf().WithStructure(" O ").Build()
                .Should().BeEquivalentTo(Array.Empty<Vector2Int>());
        }

        [Test]
        public void RangeOfFromString_WithTwoSpaces_OnSameRow()
        {
            RangeOf().WithStructure(" O O ").Build()
                .Should().BeEquivalentTo(Array.Empty<Vector2Int>());
        }

        [Test]
        public void RangeOfFromString_WithTwoSpaces_OnSameColumn()
        {
            // Note that, for formatting purposes, several whitespaces has been added to the 2nd row.
            // Mind this for the following tests. 
            RangeOf().WithStructure
                (
                    @" O 
                       O "
                )
                .Build()
                .Should().BeEquivalentTo(Array.Empty<Vector2Int>());
        }
        
        [Test]
        public void RangeOfFromString_WithBattalion_WithNoRange()
        {
            RangeOf().WithStructure(" B ").Build()
                .Should().BeEquivalentTo(Array.Empty<Vector2Int>());
        }
        
        [Test]
        public void RangeOfFromString_WithBattalion_WithAdjacentRangeAtItsRight()
        {
            RangeOf().WithStructure(" B X ").Build()
                .Should().BeEquivalentTo(new [] { new Vector2Int(1, 0)});
        }

        [Test]
        public void RangeOfFromString_WithBattalion_WithAdjacentRangeAtItsLeft()
        {
            RangeOf().WithStructure(" X B ").Build()
                .Should().BeEquivalentTo(new [] { Vector2Int.zero });
        }
        
        [Test]
        public void RangeOfFromString_WithBattalion_WithAdjacentRangesAtItsSides()
        {
            RangeOf().WithStructure(" X B X ").Build()
                .Should().BeEquivalentTo(new []
                {
                    Vector2Int.zero,
                    new Vector2Int(2, 0)
                });
        }
        
        [Test]
        public void RangeOfFromString_WithBattalion_WithRangeOfOne()
        {
            RangeOf().WithStructure
                (
                    @" O X O 
                       X B X
                       O X O"
                ).Build()
                .Should().BeEquivalentTo(new []
                {
                    new Vector2Int(1,2),
                    new Vector2Int(0, 1),
                    new Vector2Int(2, 1),
                    new Vector2Int(1,0),
                });
        }
        
        [Test]
        public void RangeOfFromString_WithBattalion_WithRangeOfTwo()
        {
            RangeOf().WithStructure
                (
                    @" O O X O O 
                       O X X X O
                       X X B X X
                       O X X X O
                       O O X O O"
                ).Build()
                .Should().BeEquivalentTo(new []
                {
                    new Vector2Int(2, 4),
                    new Vector2Int(3, 3),
                    new Vector2Int(2, 3),
                    new Vector2Int(1, 3),
                    new Vector2Int(4, 2),
                    new Vector2Int(3, 2),
                    new Vector2Int(1, 2),
                    new Vector2Int(0, 2),
                    new Vector2Int(3, 1),
                    new Vector2Int(2, 1),
                    new Vector2Int(1, 1),
                    new Vector2Int(2, 0),
                });
        }
    }
}