using System.Threading.Tasks;
using AdvanceWars.Runtime;
using AdvanceWars.Runtime.Presentation;
using FluentAssertions;
using NUnit.Framework;
using TMPro;
using UnityEngine;

namespace AdvanceWars.Tests.Runtime
{
    public class BattalionMovementTests : WithMapFixture
    {
        [Test]
        public async Task MoveBattalion()
        {
            await Task.Yield();
            await Object.FindObjectOfType<Interact>().Select();
            Object.FindObjectOfType<MoveCursorInput>().Upwards();

            await Object.FindObjectOfType<Interact>().Select();

            Object.FindObjectOfType<BattalionView>().transform.position.Should().Be(Vector3.up);
        }

        [Test]
        public async Task DeselectAfterMove()
        {
            await Task.Yield();
            await Object.FindObjectOfType<Interact>().Select();
            Object.FindObjectOfType<MoveCursorInput>().Upwards();

            await Object.FindObjectOfType<Interact>().Select();
            await Task.Yield();

            Object.FindObjectOfType<SelectionArea>().GetComponentInChildren<TMP_Text>().text.Should().Be("");
        }

        [Test]
        public async Task DoesNotMoveAfterAutomaticDeselection()
        {
            await Task.Yield();
            await Object.FindObjectOfType<Interact>().Select();
            Object.FindObjectOfType<MoveCursorInput>().Upwards();
            await Object.FindObjectOfType<Interact>().Select();
            Object.FindObjectOfType<MoveCursorInput>().Upwards();

            await Object.FindObjectOfType<Interact>().Select();
            await Task.Yield();

            Object.FindObjectOfType<BattalionView>().transform.position.Should().Be(Vector3.up);
        }

        [Test]
        public async Task CanNotMoveSameBattalionInOneTurn()
        {
            await Task.Yield();
            await Object.FindObjectOfType<Interact>().Select();
            Object.FindObjectOfType<MoveCursorInput>().Upwards();
            await Object.FindObjectOfType<Interact>().Select();
            await Object.FindObjectOfType<Interact>().Select();
            Object.FindObjectOfType<MoveCursorInput>().Upwards();

            await Object.FindObjectOfType<Interact>().Select();
            await Task.Yield();

            Object.FindObjectOfType<BattalionView>().transform.position.Should().Be(Vector3.up);
        }

        [Test]
        public async Task BattalionWait()
        {
            await Task.Yield();
            await Object.FindObjectOfType<Interact>().Select();
            
            await Object.FindObjectOfType<Interact>().Select();
            
            Object.FindObjectOfType<BattalionView>().Tired.Should().BeTrue();
        }
    }
}