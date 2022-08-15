using System.Collections.Generic;
using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;

namespace AdvanceWars.Tests
{
    public class FireTests
    {
        [Test]
        public void DefaultBaseDamage_IsZero()
        {
            var sut = new Weapon(new Dictionary<Unit, int>());
            var target = new Unit();

            sut.BaseDamageTo(target).Should().Be(0);
        }

        [Test]
        public void AssignedBaseDamage_IsGreaterThanZero()
        {
            var sut = new Weapon(new Dictionary<Unit, int>()
            {
                { new Unit(), 10 }
            });
            sut.BaseDamageTo(new Unit()).Should().Be(10);
        }

        [TestCase(40, 4)]
        [TestCase(48, 4)]
        [TestCase(9, 1)]
        public void Platoons_IsDerivated_FromStrength(int strength, int effectivity)
        {
            var sut = BatallionBuilder.Batallion().WithStrength(strength).Build();
            sut.Platoons.Should().Be(effectivity);
        }
    }
}