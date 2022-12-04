using System.Threading.Tasks;
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
        public void StartDay()
        {
            Object.FindObjectOfType<DayPanel>().GetComponentInChildren<TMP_Text>()
                .text.Should().Be("Day 1");
        }

        [Test]
        public async Task EndDayStartsNextOne()
        {
            Object.FindObjectOfType<EndTurnInput>().Interact();
            Object.FindObjectOfType<EndTurnInput>().Interact();

            await Task.Delay(1.Seconds());

            Object.FindObjectOfType<DayPanel>().GetComponentInChildren<TMP_Text>().text.Should().Be("Day 2");
        }

        [Test]
        public async Task EndTurnStartsNewTurn()
        {
            Object.FindObjectOfType<EndTurnInput>().Interact();

            await Task.Delay(1.Seconds());

            Object.FindObjectOfType<TurnPanel>().GetComponentInChildren<TMP_Text>().text.Should().Be("Nation n2");
        }

        [Test]
        public void StartTurn()
        {
            Object.FindObjectOfType<TurnPanel>().GetComponentInChildren<TMP_Text>()
                .text.Should().Be("Nation n1");
        }
    }
}