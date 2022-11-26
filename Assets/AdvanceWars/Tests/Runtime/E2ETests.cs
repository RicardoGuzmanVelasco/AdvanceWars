using System.Linq;
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
        public async Task DrawTerrains()
        {
            Object.FindObjectOfType<Button>().onClick.Invoke();

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
            Object.FindObjectOfType<Button>().onClick.Invoke();

            await Task.Yield();

            Object.FindObjectsOfType<BattalionView>()
                .Should()
                .NotBeEmpty();
        }
    }
}