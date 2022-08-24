using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;

namespace AdvanceWars.Tests
{
    public class OperationTests
    {
        [Test]
        public void FirstActiveCommandingOfficer_IsTheFirst()
        {
            var commandingOfficers = new[] { new CommandingOfficer() };
            var sut = new Operation(commandingOfficers);

            sut.ActiveCommandingOfficer.Should().Be(commandingOfficers.First());
        }

        [Test]
        public void SecondCommandingOfficer_GoesAfterTheFirst()
        {
            var commandingOfficers = new List<CommandingOfficer>()
            {
                new CommandingOfficer(),
                new CommandingOfficer()
            };
            var operation = new Operation(commandingOfficers);
            var sut = new Cursor(operation);

            sut.EndTurn();

            operation.ActiveCommandingOfficer.Should().Be(commandingOfficers.ElementAt(1));
        }

        [Test]
        public void AfterLast_ActiveCommandingOfficerIsTheFirstAgain()
        {
            var commandingOfficers = new List<CommandingOfficer>()
            {
                new CommandingOfficer(),
                new CommandingOfficer()
            };
            var operation = new Operation(commandingOfficers);
            var sut = new Cursor(operation);

            sut.EndTurn();
            sut.EndTurn();

            operation.ActiveCommandingOfficer.Should().Be(commandingOfficers.First());
        }

        [Test]
        public void OperationStartsAtDayOne()
        {
            var sut = new Operation(new List<CommandingOfficer>());
            sut.Day.Should().Be(1);
        }

        [Test]
        public void WhenEveryTurnEnds_aNewDayStarts()
        {
            var commandingOfficers = new[]
            {
                new CommandingOfficer(),
                new CommandingOfficer()
            };
            var sut = new Operation(commandingOfficers);

            sut.NextTurn();
            sut.NextTurn();

            sut.Day.Should().Be(2);
        }

        //TODO:
        //When turn starts, it starts Commanding Officer Turn. Implementation test
    }
}