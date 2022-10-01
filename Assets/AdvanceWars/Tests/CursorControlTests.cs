using AdvanceWars.Runtime.Application;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.GameBuilder;
using Cursor = AdvanceWars.Runtime.Domain.Cursor;

namespace AdvanceWars.Tests
{
    public class CursorControlTests
    {
        [Test]
        public void View_Receives_CursorMovement()
        {
            var viewMock = Substitute.For<CursorView>();
            var sut = new CursorController(Game().Build(), viewMock);

            sut.Towards(Vector2Int.right);

            viewMock.ReceivedWithAnyArgs().MoveTo(default);
        }

        [Test]
        public void CannotMoveCursor_WhenDisabled()
        {
            var viewMock = Substitute.For<CursorView>();
            var cursor = new Cursor();
            var game = Game().InjectCursor(cursor).Build();
            var sut = new CursorController(game, viewMock);

            cursor.Disable();
            sut.Towards(Vector2Int.right);

            viewMock.DidNotReceiveWithAnyArgs().MoveTo(default);
        }

        [Test]
        public void View_Receives_EnableCursor()
        {
            var cursor = new Cursor();
            cursor.Disable();
            var viewMock = Substitute.For<CursorView>();
            var sut = new CursorController(Game().InjectCursor(cursor).Build(), viewMock);

            cursor.Enable();

            viewMock.Received().Show();
        }

        [Test]
        public void View_Receives_DisableCursor()
        {
            var cursor = new Cursor();
            cursor.Enable();

            var viewMock = Substitute.For<CursorView>();
            var sut = new CursorController(Game().InjectCursor(cursor).Build(), viewMock);

            cursor.Disable();

            viewMock.Received().Hide();
        }
    }
}