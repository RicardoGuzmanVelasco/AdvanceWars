using System.Threading.Tasks;
using AdvanceWars.Runtime.Application;
using AdvanceWars.Runtime.Presentation;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace AdvanceWars.Tests.Runtime
{
    public class E2ETests
    {
        [Test]
        public async Task DrawMap()
        {
            Object.FindObjectOfType<Button>().onClick.Invoke();

            var view = Substitute.For<MapView>();
            var sut = new DrawMap(view);

            await sut.Run();

            Object.FindObjectsOfType<SpaceView>().Should().NotBeEmpty();
        }
    }
}