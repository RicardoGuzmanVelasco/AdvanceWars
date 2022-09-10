using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.MapBuilder;

namespace AdvanceWars.Tests
{
    public class MapBuilderTests
    {
        [Test]
        public void MapFromString_WithSingleSpace()
        {
            Map().WithStructure(" O ").Build()
                .Should().BeEquivalentTo(Map().Of(1, 1).Build());
        }

        [Test]
        public void MapFromString_WithTwoSpaces_OnSameRow()
        {
            Map().WithStructure(" O O ").Build()
                .Should().BeEquivalentTo(Map().Of(2, 1).Build());
        }

        [Test]
        public void MapFromString_WithTwoSpaces_OnSameColumn()
        {
            // Note that, for formatting purposes, several whitespaces has been added to the 2nd row.
            // Mind this for the following tests. 
            Map().WithStructure
                (
                    @" O 
                       O "
                )
                .Build()
                .Should().BeEquivalentTo(Map().Of(1, 2).Build());
        }
    }
}