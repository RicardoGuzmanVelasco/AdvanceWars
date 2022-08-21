using AdvanceWars.Runtime;
using AdvanceWars.Runtime.DataStructures;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace AdvanceWars.Tests.DataStructures
{
    public class xcvsdvsdasljkfdheiufghkudffTests
    {
        [Test]
        public void IsEmptyOnCreation()
        {
            var sut = new LazyBoard<Map.Space>();
            sut.Should().BeEmpty();
        }

        [Test]
        public void CreatesAdHoc()
        {
            var sut = new LazyBoard<Map.Space>();
            sut[Vector2Int.zero].Should().NotBeNull();
        }

        [Test]
        public void CreatedAdHoc_IsStored()
        {
            var sut = new LazyBoard<Map.Space>();
            var _ = sut[Vector2Int.zero];
            sut.Should().HaveCount(1);
        }

        [Test]
        public void CreateAdHoc_DoesNotDuplicate()
        {
            var sut = new LazyBoard<Map.Space>();
            _ = sut[Vector2Int.zero];
            _ = sut[Vector2Int.zero];
            sut.Should().HaveCount(1);
        }

        [Test]
        public void CoordsOfSpace()
        {
            var sut = new LazyBoard<Map.Space>();
            Map.Space space = sut[Vector2Int.one];
            sut.CoordsOf(space).Should().Be(Vector2Int.one);
        }
    }
}