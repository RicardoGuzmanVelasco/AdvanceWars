using System.Threading.Tasks;
using AdvanceWars.Runtime;
using AdvanceWars.Runtime.Presentation;
using FluentAssertions;
using NUnit.Framework;
using TMPro;
using static UnityEngine.Object;

namespace AdvanceWars.Tests.Runtime
{
    public class SelectionTests : WithMapFixture
    {
        [Test]
        public async Task Select()
        {
            await Task.Yield();
            FindObjectOfType<MoveCursorInput>().Upwards();

            await FindObjectOfType<Interact>().Select();

            await Task.Yield();

            FindObjectOfType<SelectionArea>().GetComponentInChildren<TMP_Text>().text.Should().Be("(0, 1)");
        }

        [Test]
        public async Task CancelSelection()
        {
            await Task.Yield();
            await FindObjectOfType<Interact>().Select();

            await FindObjectOfType<Interact>().Deselect();

            FindObjectOfType<SelectionArea>().GetComponentInChildren<TMP_Text>().text.Should().Be("");
        }
    }
}