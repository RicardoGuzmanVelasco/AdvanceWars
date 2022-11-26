using System.Linq;
using System.Threading.Tasks;
using AdvanceWars.Runtime;
using AdvanceWars.Runtime.Presentation;
using FluentAssertions;
using NUnit.Framework;
using TMPro;
using UnityEngine;

namespace AdvanceWars.Tests.Runtime
{
    public class DrawingMapInSceneTests : WithMapFixture
    {
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
        public async Task SelectBattalion()
        {
            await Task.Yield();

            await Object.FindObjectOfType<Interact>().Select();

            Object.FindObjectOfType<SelectionArea>().GetComponentInChildren<TMP_Text>().text.Should().Be("(0, 0)");
        }

        [Test]
        public async Task CancelSelection()
        {
            await Task.Yield();
            await Object.FindObjectOfType<Interact>().Select();

            await Object.FindObjectOfType<Interact>().Deselect();

            Object.FindObjectOfType<SelectionArea>().GetComponentInChildren<TMP_Text>().text.Should().Be("");
        }

        [Test]
        public async Task CanNotSelectUnocupiedSpace()
        {
            await Task.Yield();
            Object.FindObjectOfType<MoveCursorInput>().Upwards();

            await Object.FindObjectOfType<Interact>().Select();

            Object.FindObjectOfType<SelectionArea>().GetComponentInChildren<TMP_Text>().text.Should().Be("");
        }
    }
}