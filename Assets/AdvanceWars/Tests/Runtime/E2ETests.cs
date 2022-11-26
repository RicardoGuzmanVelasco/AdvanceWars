using System.Threading.Tasks;
using AdvanceWars.Runtime.Presentation;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AdvanceWars.Tests.Runtime
{
    public class E2ETests
    {
        [SetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("WalkingSkeleton");
        }

        [Test]
        public async Task DrawMap()
        {
            Object.FindObjectOfType<Button>().onClick.Invoke();

            await Task.Yield();

            Object.FindObjectsOfType<SpaceView>().Should().NotBeEmpty();
        }
    }
}