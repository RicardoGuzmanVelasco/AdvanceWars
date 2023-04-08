using System.Threading.Tasks;
using AdvanceWars.Runtime;
using AdvanceWars.Runtime.Inputs;
using AdvanceWars.Runtime.Presentation;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using TMPro;
using UnityEngine;

namespace AdvanceWars.Tests.Runtime
{
    public class EndTurnTests : WithMapFixture
    {
        [Test]
        public async Task StartDay()
        {
            await Task.Delay(1.Seconds());

            Object.FindObjectOfType<DayPanel>().GetComponentInChildren<TMP_Text>()
                .text.Should().Be("Day 1");
        }

        [Test]
        public async Task EndDayStartsNextOne()
        {
            await Task.Delay(1.Seconds());

            Object.FindObjectOfType<EndTurnInput>().Interact();
            await Task.Delay(1.Seconds());

            Object.FindObjectOfType<EndTurnInput>().Interact();

            await Task.Delay(1.Seconds());

            Object.FindObjectOfType<DayPanel>().GetComponentInChildren<TMP_Text>().text.Should().Be("Day 2");
        }

        [Test]
        public async Task EndTurnStartsNewTurn()
        {
            await Task.Delay(1.Seconds());

            Object.FindObjectOfType<EndTurnInput>().Interact();

            await Task.Delay(1.Seconds());

            Object.FindObjectOfType<TurnPanel>().GetComponentInChildren<TMP_Text>().text.Should().Be("Nation n2");
        }

        [Test]
        public async Task StartTurn()
        {
            await Task.Delay(1.Seconds());

            Object.FindObjectOfType<TurnPanel>().GetComponentInChildren<TMP_Text>()
                .text.Should().Be("Nation n1");
        }

        [Test]
        public async Task BattalionsAreFreshWhenNewDayStarts()
        {
            await Task.Yield();
            await Object.FindObjectOfType<Interact>().Select();
            await Object.FindObjectOfType<Interact>().Select();
            Object.FindObjectOfType<EndTurnInput>().Interact();
            await Task.Delay(1.Seconds());
            Object.FindObjectOfType<EndTurnInput>().Interact();

            await Task.Delay(1.Seconds());

            Object.FindObjectOfType<BattalionView>().Tired.Should().BeFalse();
        }

        [Test]
        public async Task DeselectAfterTurnEnds()
        {
            await Task.Yield();
            await Object.FindObjectOfType<Interact>().Select();
            Object.FindObjectOfType<EndTurnInput>().Interact();
            await Task.Delay(1.Seconds());

            Object.FindObjectOfType<EndTurnInput>().Interact();

            await Task.Delay(1.Seconds());

            Object.FindObjectOfType<SelectionArea>().GetComponentInChildren<TMP_Text>().text.Should().Be("");
        }
    }
}