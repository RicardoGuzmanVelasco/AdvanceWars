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
        public void CanNotPerform_anyManeuver_afterWait()
        {
            var sut = new CommandingOfficer();
            var battalion = Battalion().Build();

            sut.Order(Maneuver.Wait(battalion));

            sut.AvailableTacticsOf(battalion).Should().BeEmpty();
        }

        [Test]
        public void AfterFireManeuver_AutoWait()
        {
            var sut = new CommandingOfficer();
            var battalion = Battalion().Build();

            sut.Order(Maneuver.Fire(battalion));

            sut.AvailableTacticsOf(battalion).Should().BeEmpty();
        }

        [Test]
        public void Troops_CanFire_AfterMove()
        {
            var sut = new CommandingOfficer();
            var battalion = Battalion().Build();

            sut.Order(Maneuver.Move(battalion));

            sut.AvailableTacticsOf(battalion).Should().Contain(Tactic.Fire);
        }
    }
}