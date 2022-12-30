using AdvanceWars.Runtime.Extensions.DataStructures;
using FluentAssertions;
using NUnit.Framework;

namespace AdvanceWars.Tests.DataStructures
{
    public class ZeroClampedIntTests
    {
        [Test]
        public void InitialValueCanBeRetrieved()
        {
            new ZeroClampedInt(value: 1, ceil: 2).Value.Should().Be(1);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void ValueCanBeModified(int newValue)
        {
            var sut = new ZeroClampedInt(value: 0, ceil: 2);

            sut.Value = newValue;

            sut.Value.Should().Be(newValue);
        }

        [Test]
        public void ValueIsCeiled()
        {
            const int ceil = 2;
            var sut = new ZeroClampedInt(value: 1, ceil);

            const int overflowIncrement = 2;
            sut.Value += overflowIncrement;

            sut.Value.Should().Be(ceil);
        }

        [Test]
        public void ValueIsFloored()
        {
            var sut = new ZeroClampedInt(value: 0, ceil: 1);
            
            sut.Value--;
            
            sut.Value.Should().Be(0);
        }

        [Test]
        public void ValueCanBeImplicitlyConvertedToInt()
        {
            int sut = new ZeroClampedInt(value: 1, ceil: 1);

            sut.Should().Be(1);
        }
    }
}