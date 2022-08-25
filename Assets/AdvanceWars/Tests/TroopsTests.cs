using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.BattalionBuilder;

namespace AdvanceWars.Tests
{
    public class TroopsTests
    {
        [Test]
        public void anyTroop_IsFriendly_WhetherSameNation()
        {
            using var _ = new AssertionScope();

            Infantry().WithNation("aNation").Build()
                .IsAlly(Battalion().WithNation("aNation").Build())
                .Should().BeTrue();

            Infantry().WithNation("aNation").Build()
                .IsAlly(Battalion().WithNation("notSameNation").Build())
                .Should().BeFalse();
        }

        [Test]
        public void TwoStatelessTroops_AreNotFriends_EachOther()
        {
            Infantry().WithNation(null).Build()
                .IsAlly(Battalion().WithNation(null).Build())
                .Should().BeFalse();
        }
    }
}