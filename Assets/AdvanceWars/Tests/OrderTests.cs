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
        public void DefaultBattalion_CanOnlyWait()
        {
            var sut = new CommandingOfficer();
            var battalion = Battalion().Build();
            sut.AvailableTacticsOf(battalion).Should().Contain(Tactic.Wait()).And.HaveCount(1);
        }

        [Test]
        public void CanNotPerform_anyManeuver_afterWait()
        {
            var sut = new CommandingOfficer();
            var battalion = Battalion().Build();

            sut.Order(Maneuver.Wait(battalion));

            sut.AvailableTacticsOf(battalion).Should().BeEmpty();
        }

        //Aquí nos preguntamos si no debería ser que al hacer Fire se haga después automáticamente el Wait.
        [Test]
        public void AfterFireManeuver_WaitIsStillPossible()
        {
            var sut = new CommandingOfficer();
            var battalion = Battalion().Build();

            sut.Order(Maneuver.Fire(battalion));

            sut.AvailableTacticsOf(battalion).Should().Contain(Tactic.Wait()).And.HaveCount(1);
        }

        [Test]
        public void DosComandos_TEMPPPPPPPPPPPPPPPPPPP()
        {
            var sut = new CommandingOfficer();
            var battalion = Battalion().Build();

            sut.Order(Maneuver.Fire(battalion));
            sut.Order(Maneuver.Wait(battalion));

            sut.AvailableTacticsOf(battalion).Should().BeEmpty();
        }
    }
}