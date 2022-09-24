using AdvanceWars.Runtime.Application;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.GameBuilder;

namespace AdvanceWars.Tests
{
    public class MoveCursorTests
    {
        [Test]
        public void View_Receives_CursorMovement()
        {
            var viewMock = Substitute.For<CursorView>();
            var sut = new MoveCursorController(Game().Build(), viewMock);

            sut.Towards(Vector2Int.right);

            viewMock.ReceivedWithAnyArgs().MoveTo(default);
        }
    }
}