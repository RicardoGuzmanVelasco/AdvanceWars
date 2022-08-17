using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.BatallionBuilder;

namespace AdvanceWars.Tests
{
    public class TroopsTests
    {
        [Test]
        public void aUnit_IsFriendly_WhetherSameMotherland()
        {
            using var _ = new AssertionScope();

            Infantry().Friend().Build()
                .IsFriend(Infantry().Friend().Build())
                .Should().BeTrue();

            Infantry().Friend().Build()
                .IsFriend(Infantry().Enemy().Build())
                .Should().BeFalse();
        }

        //"Coalición" es un conjunto de naciones aliadas, para cuando haya 2vs2.
        //Apátrida: una unidad sin nación. 
        //2 apatridas no deberian compartir nacion
    }
}