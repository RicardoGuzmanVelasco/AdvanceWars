using System.Linq;
using System.Threading.Tasks;
using AdvanceWars.Runtime.Presentation;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace AdvanceWars.Tests.Runtime
{
    public class DrawingMapInSceneTests : WithMapFixture
    {
        [Test]
        public async Task DrawTerrains()
        {
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
            await Task.Yield();

            Object.FindObjectsOfType<BattalionView>()
                .Should()
                .NotBeEmpty();
        }
    }
}