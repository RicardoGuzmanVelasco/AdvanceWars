using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.CommandingOfficerBuilder;

namespace AdvanceWars.Tests
{
    public class OperationTests
    {
        [Test]
        public void FirstActiveCommandingOfficer_IsTheFirst()
        {
            var commandingOfficers = new[] { CommandingOfficer().Build() };
            var sut = new Operation(commandingOfficers);

            sut.ActiveCommandingOfficer.Should().Be(commandingOfficers.First());
        }

        [Test]
        public void SecondCommandingOfficer_GoesAfterTheFirst()
        {
            var commandingOfficers = new[]
            {
                CommandingOfficer().Of(new Nation("1")).Build(),
                CommandingOfficer().Of(new Nation("2")).Build()
            };
            var operation = new Operation(commandingOfficers);
            var sut = new Cursor(operation);

            sut.EndTurn();

            operation.ActiveCommandingOfficer.Should().Be(commandingOfficers[1]);
        }

        [Test]
        public void AfterLast_ActiveCommandingOfficerIsTheFirstAgain()
        {
            var commandingOfficers = new List<CommandingOfficer>()
            {
                CommandingOfficer().Build(),
                CommandingOfficer().Build()
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
            var sut = new Operation(new[] { CommandingOfficer().Build() });
            sut.Day.Should().Be(1);
        }

        [Test]
        public void WhenEveryTurnEnds_aNewDayStarts()
        {
            var commandingOfficers = new[]
            {
                CommandingOfficer().Build(),
                CommandingOfficer().Build()
            };
            var sut = new Operation(commandingOfficers);

            sut.NextTurn();
            sut.NextTurn();

            sut.Day.Should().Be(2);
        }

        [Test]
        public void NewTurnOfDay_IsRaised_OnNewTurn()
        {
            var commandingOfficers = new[]
            {
                CommandingOfficer().Of(new Nation("A")).Build(),
                CommandingOfficer().Of(new Nation("B")).Build()
            };

            var sut = new Operation(commandingOfficers);

            using var monitoredSut = sut.Monitor();
            sut.NextTurn();

            monitoredSut
                .Should().Raise(nameof(sut.NewTurnOfDay))
                .WithArgs<NewTurnOfDayArgs>(args => args.Nation.Id.Equals("B") && args.Day.Equals(1));
        }

        //TODO:
        //When turn starts, it starts Commanding Officer Turn. Implementation test
    }
}