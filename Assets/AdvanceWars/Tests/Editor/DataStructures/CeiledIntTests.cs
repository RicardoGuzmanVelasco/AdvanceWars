using AdvanceWars.Runtime.Extensions.DataStructures;
using FluentAssertions;
using NUnit.Framework;

namespace AdvanceWars.Tests.DataStructures
{
    public class CeiledIntTests
    {
        [Test]
        public void InitialValueCanBeRetrieved()
        {
            new CeiledInt(value: 1, ceil: 2).Value.Should().Be(1);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void ValueCanBeModified(int newValue)
        {
            var sut = new CeiledInt(value: 0, ceil: 2);
            
            sut.Value = newValue;

            sut.Value.Should().Be(newValue);
        }
        
        [Test]
        public void ValueIsCeiled()
        {
            const int ceil = 2;
            var sut = new CeiledInt(value: 1, ceil);

            const int overflowIncrement = 2;
            sut.Value += overflowIncrement;

            sut.Value.Should().Be(ceil);
        }
    }
}