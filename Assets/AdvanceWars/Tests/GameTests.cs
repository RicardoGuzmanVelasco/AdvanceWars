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
            var monitoredSut = sut.Monitor();
            sut.EndCurrentTurn();
            sut.BeginNextTurn();

            sut.ActivePlayer.Id.Should().Be("B");

            monitoredSut.Should().Raise(nameof(sut.CursorEnableChanged))
                .WithArgs<bool>(enabled => enabled);
        }

        [Test]
        public void WhenAlreadyEnabled_CursorCanNotBeEnabled()
        {
            var anyPlayers = new Dictionary<Nation, Player>()
            {
                { new Nation("A"), new Player { Id = "A" } },
            };

            var sut = new Game(CommandingOfficers(1), anyPlayers);

            sut.BeginNextTurn();
            var monitoredSut = sut.Monitor();
            sut.BeginNextTurn();

            monitoredSut.Should().NotRaise(nameof(sut.CursorEnableChanged));
        }

        [Test]
        public void WhenAlreadyDisabled_CursorCanNotBeDisabled()
        {
            var anyPlayers = new Dictionary<Nation, Player>()
            {
                { new Nation("A"), new Player { Id = "A" } },
            };

            var sut = new Game(CommandingOfficers(1), anyPlayers);

            sut.EndCurrentTurn();
            var monitoredSut = sut.Monitor();
            sut.EndCurrentTurn();

            monitoredSut.Should().NotRaise(nameof(sut.CursorEnableChanged));
        }

        // ¿Debería el evento de estar en el Cursor y tener un puente en el Game?
        [Test]
        public void OnGameBegin_CursorEnabledChanged_ShouldBeRaised()
        {
            var sut = new Game(CommandingOfficers(1), new Dictionary<Nation, Player>());

            var monitoredSut = sut.Monitor();

            sut.Begin();

            monitoredSut.Should().Raise(nameof(sut.CursorEnableChanged))
                .WithArgs<bool>(enabled => enabled);
        }

        // Implicitamente estamos testeando que no se haga begin 2 veces, aunque estemos
        // testeando el cursor. Cuidao.
        [Test]
        public void GameShouldNotBeginTwice()
        {
            var sut = new Game(CommandingOfficers(1), new Dictionary<Nation, Player>());

            sut.Begin();
            var monitoredSut = sut.Monitor();
            sut.Begin();

            monitoredSut.Should().NotRaise(nameof(sut.CursorEnableChanged));
        }
    }
}