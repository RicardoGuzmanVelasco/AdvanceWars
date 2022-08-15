using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;

namespace AdvanceWars.Tests
{
    public class FireTests
    {
        [Test]
        public void METHOD()
        {
            var sut = new Weapon();
            var target = new Unit();

            sut.BaseDamageTo(target).Should().Be(0);
        }
    }
}