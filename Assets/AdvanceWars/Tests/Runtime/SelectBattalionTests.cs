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
    public class SelectBattalionTests : WithMapFixture
    {
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

        [Test]
        public async Task MoveBattalion()
        {
            await Task.Yield();
            await Object.FindObjectOfType<Interact>().Select();
            Object.FindObjectOfType<MoveCursorInput>().Upwards();
            
            await Object.FindObjectOfType<Interact>().Select();

            Object.FindObjectsOfType<BattalionView>().Where(x => x.transform.position == Vector3.up).Should().ContainSingle();
        }
    }
}