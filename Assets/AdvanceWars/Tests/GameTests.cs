using System.Collections.Generic;
using AdvanceWars.Runtime;
using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.CommandingOfficerBuilder;

namespace AdvanceWars.Tests
{
    public class GameTests
    {
        [Test]
        public void NewTurnOfDay_IsRaised_AfterTurnEnds()
        {
            var commandingOfficers = new[]
            {
                CommandingOfficer().Of(new Nation("A")).Build(),
                CommandingOfficer().Of(new Nation("B")).Build()
            };

            var players = new Dictionary<Nation, Player>
            {
                { new Nation("A"), new Player { Id = "A" } },
            };
            var sut = new Game(commandingOfficers, players);

            using var monitoredSut = sut.Monitor();
            sut.EndCurrentTurn();

            monitoredSut
                .Should().Raise(nameof(sut.NewTurnOfDay))
                .WithArgs<NewTurnOfDayArgs>(args => args.Nation.Id.Equals("B") && args.Day.Equals(1));
        }

        [Test]
        public void OnFirstTurn_TheFirstPlayerIsTheActiveOne()
        {
            var commandingOfficers = new[]
            {
                CommandingOfficer().Of(new Nation("A")).Build(),
                CommandingOfficer().Of(new Nation("B")).Build()
            };
            var players = new Dictionary<Nation, Player>()
            {
                { new Nation("A"), new Player { Id = "A" } },
                { new Nation("B"), new Player { Id = "B" } }
            };

            var sut = new Game(commandingOfficers, players);

            sut.ActivePlayer.Id.Should().Be("A");
        }

        [Test]
        public void AfterBegin_aNewTurn_NextPlayerIsTheActiveOne()
        {
            var commandingOfficers = new[]
            {
                CommandingOfficer().Of(new Nation("A")).Build(),
                CommandingOfficer().Of(new Nation("B")).Build()
            };
            var players = new Dictionary<Nation, Player>()
            {
                { new Nation("A"), new Player { Id = "A" } },
                { new Nation("B"), new Player { Id = "B" } }
            };

            var sut = new Game(commandingOfficers, players);
            sut.EndCurrentTurn();
            sut.BeginNextTurn();

            sut.ActivePlayer.Id.Should().Be("B");
            sut.CursorIsEnabled.Should().BeTrue();
        }

        //cursor tiene que estar activo. llevar a otro test.
    }
}