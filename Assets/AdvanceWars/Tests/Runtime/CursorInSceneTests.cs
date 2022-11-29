using System;
using AdvanceWars.Runtime;
using AdvanceWars.Runtime.Presentation;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AdvanceWars.Tests.Runtime
{
    public class CursorInSceneTests : WithMapFixture
    {
        [Test]
        public void CursorStartsAtZero()
        {
            Object.FindObjectOfType<Selector>()
                .transform.position
                .Should().Be(Vector3.zero);
        }

        [Test]
        public void MoveCursorUpwards()
        {
            Object.FindObjectOfType<MoveCursorInput>().Upwards();

            Object.FindObjectOfType<Selector>()
                .transform.position
                .Should().Be(Vector3.up);
        }

        [Test]
        public void MoveCursorDownwards()
        {
            Object.FindObjectOfType<MoveCursorInput>().Upwards();

            Object.FindObjectOfType<MoveCursorInput>().Downwards();

            Object.FindObjectOfType<Selector>()
                .transform.position
                .Should().Be(Vector3.zero);
        }

        [Test]
        public void MoveCursorLeftwards()
        {
            Object.FindObjectOfType<MoveCursorInput>().Rightwards();

            Object.FindObjectOfType<MoveCursorInput>().Leftwards();

            Object.FindObjectOfType<Selector>()
                .transform.position
                .Should().Be(Vector3.zero);
        }

        [Test]
        public void MoveCursorRightwards()
        {
            Object.FindObjectOfType<MoveCursorInput>().Rightwards();

            Object.FindObjectOfType<Selector>()
                .transform.position
                .Should().Be(Vector3.right);
        }

        [Test]
        public void MoveCursor_aDiagonal()
        {
            Object.FindObjectOfType<MoveCursorInput>().Upwards();
            Object.FindObjectOfType<MoveCursorInput>().Rightwards();

            Object.FindObjectOfType<Selector>()
                .transform.position
                .Should().Be(Vector3.up + Vector3.right);
        }

        [Test]
        public void CanNotMoveOutOfBounds()
        {
            Action act = () => Object.FindObjectOfType<MoveCursorInput>().Leftwards();

            act.Should().NotThrow();
            Object.FindObjectOfType<MoveCursorInput>()
                .transform.position
                .Should().Be(Vector3.zero);
        }
    }
}