using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.BattalionBuilder;

namespace AdvanceWars.Tests
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void DefaultBattallion_CanOnlyWait()
        {
            var sut = new CommandingOfficer();
            var battalion = Battalion().Build();
            sut.AvailableTacticsOf(battalion).Should().Contain(new Tactic("Wait")).And.HaveCount(1);
        }

        [Test]
        public void Order_aManeuver()
        {
            var sut = new CommandingOfficer();
            var battalion = Battalion().Build();

            sut.Order(new Maneuver(battalion));

            sut.Maneuvers.Should().ContainSingle();
        }
    }
}