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
    }
}