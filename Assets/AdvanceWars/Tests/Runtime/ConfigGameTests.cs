using System.Collections;
using System.Threading.Tasks;
using AdvanceWars.Runtime.Presentation;
using AdvanceWars.Runtime.Presenters;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using TMPro;
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

            FindObjectOfType<Button>().onClick?.Invoke();
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
            FindObjectOfType<Button>().onClick?.Invoke();
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
        public void AddPlayer()
        {
            FindObjectOfType<PlayerAmountInput>().Add();

            FindObjectOfType<PlayerAmountText>().GetComponentInChildren<TMP_Text>().text.Should().Be("Players: 2");
        }
        
        [Test]
        public async Task AddPlayerAmount()
        {
            FindObjectOfType<PlayerAmountInput>().Add();
        }
    }
}