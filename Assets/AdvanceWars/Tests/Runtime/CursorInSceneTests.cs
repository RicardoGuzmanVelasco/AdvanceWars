using System;
using System.Threading.Tasks;
using AdvanceWars.Runtime;
using AdvanceWars.Runtime.Presentation;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using static UnityEngine.Object;

namespace AdvanceWars.Tests.Runtime
{
    public class CursorInSceneTests : WithMapFixture
    {
        [Test]
        public void CursorStartsAtZero()
        {
            FindObjectOfType<Selector>()
                .transform.position
                .Should().Be(Vector3.zero);
        }

        [Test]
        public void MoveCursorUpwards()
        {
            FindObjectOfType<MoveCursorInput>().Upwards();

            FindObjectOfType<Selector>()
                .transform.position
                .Should().Be(Vector3.up);
        }

        [Test]
        public void MoveCursorDownwards()
        {
            FindObjectOfType<MoveCursorInput>().Upwards();

            FindObjectOfType<MoveCursorInput>().Downwards();

            FindObjectOfType<Selector>()
                .transform.position
                .Should().Be(Vector3.zero);
        }

        [Test]
        public void MoveCursorLeftwards()
        {
            FindObjectOfType<MoveCursorInput>().Rightwards();

            FindObjectOfType<MoveCursorInput>().Leftwards();

            FindObjectOfType<Selector>()
                .transform.position
                .Should().Be(Vector3.zero);
        }

        [Test]
        public void MoveCursorRightwards()
        {
            FindObjectOfType<MoveCursorInput>().Rightwards();

            FindObjectOfType<Selector>()
                .transform.position
                .Should().Be(Vector3.right);
        }

        [Test]
        public void MoveCursor_aDiagonal()
        {
            FindObjectOfType<MoveCursorInput>().Upwards();
            FindObjectOfType<MoveCursorInput>().Rightwards();

            FindObjectOfType<Selector>()
                .transform.position
                .Should().Be(Vector3.up + Vector3.right);
        }

        [Test]
        public void CanNotMoveOutOfBounds()
        {
            Action act = () => FindObjectOfType<MoveCursorInput>().Leftwards();

            act.Should().NotThrow();
            FindObjectOfType<MoveCursorInput>()
                .transform.position
                .Should().Be(Vector3.zero);
        }

        [Test]
        public void StartDay()
        {
            FindObjectOfType<DayPanel>().GetComponentInChildren<TMP_Text>()
                .text.Should().Be("Day 1");
        }

        [Test]
        public async Task EndDayStartsNextOne()
        {
            FindObjectOfType<EndTurnInput>().Interact();

            await Task.Delay(1.Seconds());

            FindObjectOfType<DayPanel>().GetComponentInChildren<TMP_Text>().text.Should().Be("Day 2");
        }
    }
}