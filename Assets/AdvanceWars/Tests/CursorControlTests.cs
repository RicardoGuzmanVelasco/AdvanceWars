using AdvanceWars.Runtime.Application;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.GameBuilder;

namespace AdvanceWars.Tests
{
    public class CursorControlTests
    {
        [Test]
        public void View_Receives_CursorMovement()
        {
            var viewMock = Substitute.For<CursorView>();
            var sut = new CursorMovement(Game().Build(), viewMock);

            sut.Towards(Vector2Int.right);

            viewMock.ReceivedWithAnyArgs().MoveTo(default);
        }

        [Test]
        public void View_Receives_EnableCursor()
        {
            var game = Game().Build();
            var viewMock = Substitute.For<CursorView>();
            var sut = new CursorRendering(game, viewMock);

            game.BeginNextTurn(); //Side effect: cursor is enabled.

            viewMock.Received().Show();
        }

        [Test]
        public void View_Receives_DisableCursor()
        {
            var game = Game().Build();
            var viewMock = Substitute.For<CursorView>();
            var sut = new CursorRendering(game, viewMock);

            game.BeginNextTurn();
            game.EndCurrentTurn(); //Side effect: cursor is disabled.

            viewMock.Received().Hide();
        }
    }
}