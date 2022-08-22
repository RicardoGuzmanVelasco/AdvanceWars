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
        public void AvailableTacticsOf_NewBatallion_IsNotEmpty()
        {
            var sut = new CommandingOfficer();
            var batallion = Battalion().Build();
            sut.AvailableTacticsOf(batallion).Should().NotBeEmpty();
        }

        [Test]
        public void Order_aManeuver()
        {
            var sut = new CommandingOfficer();
            var batallion = Battalion().Build();

            sut.Order(new Maneuver(batallion));

            sut.Maneuvers.Should().ContainSingle();
        }
    }
}