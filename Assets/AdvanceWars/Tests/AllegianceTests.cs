using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.BattalionBuilder;

namespace AdvanceWars.Tests
{
    public class AllegianceTests
    {
        [Test]
        public void TwoNonStateless_WithDifferentNations_AreEnemies()
        {
            var sut = Battalion().WithNation("Any").Build();
            var enemy = Battalion().WithNation("AnyOther").Build();

            sut.IsEnemy(enemy).Should().BeTrue();
            sut.IsNeutral(enemy).Should().BeFalse();
            sut.IsAlly(enemy).Should().BeFalse();
        }

        [Test]
        public void TwoNonStateless_WithSameNations_AreAllies()
        {
            var sut = Battalion().WithNation("Any").Build();
            var enemy = Battalion().WithNation("Any").Build();

            sut.IsAlly(enemy).Should().BeTrue();
            sut.IsEnemy(enemy).Should().BeFalse();
            sut.IsNeutral(enemy).Should().BeFalse();
        }

        [Test]
        public void Stateless_IsNeutralWithAnyNation()
        {
            var sut = Battalion().Build();
            var other = Battalion().WithNation("Any").Build();

            sut.IsNeutral(other).Should().BeTrue();
            sut.IsEnemy(other).Should().BeFalse();
            sut.IsAlly(other).Should().BeFalse();
        }

        [Test]
        public void RelationshipWith_IsCommutative()
        {
            var sut = Battalion().WithNation("Any").Build();
            var other = Battalion().Build();

            sut.IsNeutral(other).Should().BeTrue();
            sut.IsEnemy(other).Should().BeFalse();
            sut.IsAlly(other).Should().BeFalse();
        }
    }
}