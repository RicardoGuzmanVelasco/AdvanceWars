using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using static AdvanceWars.Tests.Builders.BatallionBuilder;

namespace AdvanceWars.Tests
{
    public class TroopsTests
    {
        [Test]
        public void anyTroop_IsFriendly_WhetherSameNation()
        {
            using var _ = new AssertionScope();

            Infantry().WithNation("aNation").Build()
                .IsFriend(Batallion().WithNation("aNation").Build())
                .Should().BeTrue();

            Infantry().WithNation("aNation").Build()
                .IsFriend(Batallion().WithNation("notSameNation").Build())
                .Should().BeFalse();
        }

        [Test]
        public void TwoStatelessTroops_AreNotFriends_EachOther()
        {
            Infantry().WithNation(null).Build()
                .IsFriend(Batallion().WithNation(null).Build())
                .Should().BeFalse();
        }

        //"Coalición" es un conjunto de naciones aliadas, para cuando haya 2vs2.
        //Apátrida: una unidad sin nación. 
        //2 apatridas no deberian compartir nacion
    }
}