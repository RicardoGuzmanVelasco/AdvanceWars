using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using AdvanceWars.Runtime.Presentation;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace AdvanceWars.Tests.Runtime
{
    public class DrawingInMapTests
    {
        [UnitySetUp]
        public IEnumerator LoadScene()
        {
            SceneManager.LoadScene("WalkingSkeleton");
            yield return null;
            Object.FindObjectOfType<Button>().onClick.Invoke();
            yield return null;
        }

        [Test]
        public async Task DrawTerrains()
        {
            await Task.Yield();

            Object.FindObjectsOfType<SpaceView>().Select(x => x.GetComponent<SpriteRenderer>())
                .Should()
                .HaveCount(2).And
                .Contain(s => s.color == Color.green).And
                .Contain(s => s.color == Color.yellow);
        }

        [Test]
        public async Task DrawBattalion()
        {
            await Task.Yield();

            Object.FindObjectsOfType<BattalionView>()
                .Should()
                .NotBeEmpty();
        }

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
        public void MoveCursorLeft()
        {
            Object.FindObjectOfType<MoveCursorInput>().Rightwards();

            Object.FindObjectOfType<MoveCursorInput>().Leftwards();

            Object.FindObjectOfType<Selector>()
                .transform.position
                .Should().Be(Vector3.zero);
        }

        [Test]
        public void MoveCursorRight()
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
    }
}