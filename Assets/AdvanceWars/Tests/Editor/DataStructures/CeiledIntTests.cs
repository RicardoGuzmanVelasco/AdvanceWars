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

        [Test]
        public void ValueCanBeImplicitlyConvertedToInt()
        {
            int sut = new CeiledInt(value: 1, ceil: 1);

            sut.Should().Be(1);
        }

        [Test]
        public void InitialValueCanNotOverflowCeil()
        {
            new CeiledInt(value: 2, ceil: 1).Value.Should().Be(1);
        }

        [Test]
        public void CeiledInt_PlusInt_DoesNotOverflow()
        {
            var sut = new CeiledInt(value: 1, ceil: 1) + 1;

            sut.Value.Should().Be(1);
        }
        
        [Test]
        public void Int_PlusCeiledInt_DoesNotOverflow()
        {
            var sut = 1 + new CeiledInt(value: 1, ceil: 1);

            sut.Value.Should().Be(1);
        }
    }
}