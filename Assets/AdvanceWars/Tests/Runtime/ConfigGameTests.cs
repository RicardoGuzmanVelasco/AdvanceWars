using System.Collections;
using System.Threading.Tasks;
using AdvanceWars.Runtime;
using AdvanceWars.Runtime.Presentation;
using AdvanceWars.Runtime.Presenters;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using static UnityEngine.Object;

namespace AdvanceWars.Tests.Runtime
{
    public class ConfigGameTests
    {
        [UnitySetUp]
        public IEnumerator LoadScene()
        {
            yield return null;
            SceneManager.LoadScene("ModeSelection");
            yield return null;
        }

        [Test]
        public async Task DefaultStartGameWithOnePlayer()
        {
            FindObjectOfType<LoadGameInput>().Interact();
            await Task.Delay(3000);

            FindObjectOfType<EndTurnInput>().Interact();
            await Task.Delay(1.Seconds());

            FindObjectOfType<DayPanel>().GetComponentInChildren<TMP_Text>().text.Should().Be("Day 2");
        }

        [Test]
        public async Task CreateGameWithMorePlayers()
        {
            FindObjectOfType<PlayerAmountInput>().Add();
            FindObjectOfType<PlayerAmountInput>().Add();
            FindObjectOfType<LoadGameInput>().Interact();
            await Task.Delay(3000);
            FindObjectOfType<EndTurnInput>().Interact();
            await Task.Delay(1.Seconds());
            FindObjectOfType<EndTurnInput>().Interact();
            await Task.Delay(1.Seconds());

            FindObjectOfType<TurnPanel>().GetComponentInChildren<TMP_Text>().text.Should().Be("Nation n3");
        }

        [Test]
        public void DefaultPlayerAmountIsOne()
        {
            FindObjectOfType<PlayerAmountText>().GetComponentInChildren<TMP_Text>().text.Should().Be("Players: 1");
        }

        [Test]
        public async Task AddPlayer()
        {
            FindObjectOfType<PlayerAmountInput>().Add();
            await Task.Yield();
            FindObjectOfType<PlayerAmountText>().GetComponentInChildren<TMP_Text>().text.Should().Be("Players: 2");
        }

        [Test]
        public async Task AddPlayerAmount()
        {
            FindObjectOfType<PlayerAmountInput>().Add();
            await Task.Yield();
        }

        [Test]
        public async Task RemovePlayer()
        {
            FindObjectOfType<PlayerAmountInput>().Add();
            FindObjectOfType<PlayerAmountInput>().Remove();
            await Task.Yield();
            FindObjectOfType<PlayerAmountText>().GetComponentInChildren<TMP_Text>().text.Should().Be("Players: 1");
        }

        [Test]
        public async Task MayNotRemoveOnlyPlayer()
        {
            FindObjectOfType<PlayerAmountInput>().Remove();
            await Task.Yield();
            FindObjectOfType<PlayerAmountText>().GetComponentInChildren<TMP_Text>().text.Should().Be("Players: 1");
        }

        [Test]
        public async Task MoveBattalion_AfterLoad()
        {
            FindObjectOfType<PlayerAmountInput>().Add();
            FindObjectOfType<PlayerAmountInput>().Add();
            FindObjectOfType<LoadGameInput>().Interact();
            await Task.Delay(3000);

            await FindObjectOfType<Interact>().Select();
            FindObjectOfType<MoveCursorInput>().Upwards();
            await FindObjectOfType<Interact>().Select();
            await Task.Delay(1.Seconds());

            FindObjectOfType<BattalionView>().transform.position.Should().Be(Vector3.up);
        }
    }
}