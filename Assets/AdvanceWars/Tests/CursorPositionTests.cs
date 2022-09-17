using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.GameBuilder;
using static AdvanceWars.Tests.Builders.MapBuilder;

namespace AdvanceWars.Tests
{
    public class CursorPositionTests
    {
        [Test]
        public void IsInZero_ByDefault()
        {
            Game().On(Map().Of(2).Build()).Build()
                .CursorCoord
                .Should().Be(Vector2Int.zero);
        }

        [Test]
        public void PutsInOtherCoord()
        {
            var sut = Game().On(Map().Of(2).Build()).Build();

            sut.PutCursorAt(Vector2Int.one);

            sut.CursorCoord.Should().Be(Vector2Int.one);
        }

        [Test]
        public void PutInOtherCoord_RaiseEvent()
        {
            var sut = Game().On(Map().Of(2).Build()).Build();
            var sutSpy = sut.Monitor();

            sut.PutCursorAt(Vector2Int.one);

            sutSpy.Should()
                .Raise(nameof(sut.CursorMoved))
                .WithArgs<Vector2Int>(c => c == Vector2Int.one);
        }

        [Test]
        public void MoveTo_Left()
        {
            var sut = Game().On(Map().Of(3).Build()).Build();
            sut.PutCursorAt(Vector2Int.one);

            sut.MoveCursorTo(Vector2Int.left);

            sut.CursorCoord.Should().Be(new Vector2Int(0, 1));
        }

        [Test]
        public void MoveTo_Right()
        {
            var sut = Game().On(Map().Of(3).Build()).Build();
            sut.PutCursorAt(Vector2Int.one);

            sut.MoveCursorTo(Vector2Int.right);

            sut.CursorCoord.Should().Be(new Vector2Int(2, 1));
        }

        [Test]
        public void MoveTo_Up()
        {
            var sut = Game().On(Map().Of(3).Build()).Build();
            sut.PutCursorAt(Vector2Int.one);

            sut.MoveCursorTo(Vector2Int.up);

            sut.CursorCoord.Should().Be(new Vector2Int(1, 2));
        }

        [Test]
        public void MoveTo_Down()
        {
            var sut = Game().On(Map().Of(3).Build()).Build();
            sut.PutCursorAt(Vector2Int.one);

            sut.MoveCursorTo(Vector2Int.down);

            sut.CursorCoord.Should().Be(new Vector2Int(1, 0));
        }

        [Test]
        public void MoveTo_Elsewhere_RaiseEvent()
        {
            var sut = Game().On(Map().Of(3).Build()).Build();
            sut.PutCursorAt(Vector2Int.one);
            var sutSpy = sut.Monitor();

            sut.MoveCursorTo(Vector2Int.down);

            sutSpy.Should().Raise(nameof(sut.CursorMoved));
        }
    }
}