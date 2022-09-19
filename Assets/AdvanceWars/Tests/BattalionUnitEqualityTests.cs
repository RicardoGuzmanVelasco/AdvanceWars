using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.BattalionBuilder;

namespace AdvanceWars.Tests
{
    public class BattalionUnitEqualityTests
    {
        [Test]
        public void BattalionUnit_Equals_OtherBattalionUnit()
        {
            var sut = Battalion().WithNation("sameNation").WithForces(100).WithMoveRate(1).Build();
            var other = Battalion().WithNation("sameNation").WithForces(10).WithMoveRate(1).Build();

            sut.EqualUnit(other).Should().Be(true);
        }
    }
}