using System.Collections;
using System.Threading.Tasks;
using AdvanceWars.Runtime.Presentation;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

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
            Object.FindObjectOfType<LoadGameInput>().Interact();
            await Task.Delay(3000);

            Object.FindObjectOfType<Button>().onClick?.Invoke();
            Object.FindObjectOfType<EndTurnInput>().Interact();
            await Task.Delay(1.Seconds());

            Object.FindObjectOfType<DayPanel>().GetComponentInChildren<TMP_Text>().text.Should().Be("Day 2");

        }
    }
}