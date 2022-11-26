using System.Linq;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Troops;
using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.CommandingOfficerBuilder;

namespace AdvanceWars.Tests
{
    public class OperationTests
    {
        private Nation Ally => new Nation("Ally");
        private Nation Enemy => new Nation("Enemy");
        private Nation SomeNation => new Nation("A");
        private int SomeForces => 50;

        [Test]
        public void FirstActiveCommandingOfficer_IsTheFirst()
        {
            var officers = CommandingOfficers(1);
            var sut = new Operation(officers);

            sut.NationInTurn.Should().Be(officers.First().Motherland);
        }

        [Test]
        public void SecondCommandingOfficer_GoesAfterTheFirst()
        {
            var officers = CommandingOfficers(2);
            var sut = new Operation(officers);

            sut.EndTurn();

            sut.NationInTurn.Should().Be(officers[1].Motherland);
        }

        [Test]
        public void AfterLast_ActiveCommandingOfficerIsTheFirstAgain()
        {
            var officers = CommandingOfficers(2);
            var sut = new Operation(officers);

            sut.EndTurn();
            sut.EndTurn();

            sut.NationInTurn.Should().Be(officers.First().Motherland);
        }

        [Test]
        public void OperationStartsAtDayOne()
        {
            var sut = new Operation(CommandingOfficers(1));
            sut.Day.Should().Be(1);
        }

        [Test]
        public void WhenTurnOfEachNationEnds_aNewDayStarts()
        {
            var commandingOfficers = CommandingOfficers(2);
            var sut = new Operation(commandingOfficers);

            sut.EndTurn();
            sut.EndTurn();

            sut.Day.Should().Be(2);
        }
    }
}