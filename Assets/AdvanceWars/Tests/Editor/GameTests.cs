using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Troops;
using FluentAssertions;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.GameBuilder;

namespace AdvanceWars.Tests
{
    public class GameTests
    {
        [Test]
        public void NewTurnOfDay_IsRaised_AfterTurnEnds()
        {
            var sut = Game().WithNations("A", "B").Began().Build();

            using var monitoredSut = sut.Monitor();
            sut.EndCurrentTurn();

            monitoredSut
                .Should().Raise(nameof(sut.NewTurnOfDay))
                .WithArgs<NewTurnOfDayArgs>(args => args.Nation.Equals(new Nation("B")) && args.Day.Equals(1));
        }

        [Test]
        public void WhenGameBegins_ThePlayerAssociatedToTheFirstCommandingOfficer_IsTheActiveOne()
        {
            var sut = Game().WithNations("A", "B").Began().Build();

            sut.ActivePlayer.Id.Should().Be("A");
        }

        [Test]
        public void OnNextTurn_NextPlayerIsTheActiveOne()
        {
            var sut = Game().WithNations("A", "B").Began().Build();

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
            var sut = Game().Of(1).Began().Build();

            sut.BeginNextTurn();
            var monitoredSut = sut.Monitor();
            sut.BeginNextTurn();

            monitoredSut.Should().NotRaise(nameof(sut.CursorEnableChanged));
        }

        [Test]
        public void WhenAlreadyDisabled_CursorCanNotBeDisabled()
        {
            var sut = Game().Of(1).Began().Build();

            sut.EndCurrentTurn();
            var monitoredSut = sut.Monitor();
            sut.EndCurrentTurn();

            monitoredSut.Should().NotRaise(nameof(sut.CursorEnableChanged));
        }

        [Test]
        public void GameShouldNotBeginTwice()
        {
            var sut = Game().Of(1).Began().Build();

            var monitoredSut = sut.Monitor();
            sut.Begin();

            monitoredSut.Should().NotRaise(nameof(sut.CursorEnableChanged));
        }

        [Test]
        public void GameBeginsAtFirstTurn()
        {
            var sut = Game().Of(2).Began().Build();
            sut.Begin();

            sut.FirstTurnOfDay
                .Should().BeTrue();
        }
        
        [Test]
        public void FirstTurnOfDayOnSecondDay()
        {
            var sut = Game().Of(1).Began().Build();
            sut.Begin();

            sut.EndTurn();
            
            sut.FirstTurnOfDay.Should().BeTrue();
        }
    }
}