using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.CommandingOfficerBuilder;

namespace AdvanceWars.Tests
{
    public class GameTests
    {
        [Test]
        public void NewTurnOfDay_IsRaised_OnNewTurn()
        {
            var commandingOfficers = new[]
            {
                CommandingOfficer().Of(new Nation("A")).Build(),
                CommandingOfficer().Of(new Nation("B")).Build()
            };

            var sut = new Game(commandingOfficers);

            using var monitoredSut = sut.Monitor();
            sut.NextTurn();

            monitoredSut
                .Should().Raise(nameof(sut.NewTurnOfDay))
                .WithArgs<NewTurnOfDayArgs>(args => args.Nation.Id.Equals("B") && args.Day.Equals(1));
        }
    }
}