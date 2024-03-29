﻿using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using static AdvanceWars.Tests.Builders.BuildingBuilder;
using static AdvanceWars.Tests.Builders.SituationBuilder;

namespace AdvanceWars.Tests
{
    public class EconomyTests
    {
        private Nation SomeNation => new Nation("A");

        [Test]
        public void EarnWarFunds()
        {
            var sut = new Treasury();

            sut.Earn(1000);

            sut.WarFunds.Should().Be(1000);
        }

        [Test]
        public void SpendWarFunds()
        {
            var sut = new Treasury(1000);

            sut.Spend(250);

            sut.WarFunds.Should().Be(750);
        }

        [TestCase(1001, false)]
        [TestCase(999, true)]
        [TestCase(1000, true)]
        public void CanAfford(int amount, bool result)
        {
            var sut = new Treasury(1000);

            sut.CanAfford(amount)
                .Should().Be(result);
        }

        [Test]
        public void IncomeFromAllyBuilding()
        {
            var map = new Map(1, 1);
            map.Put(Vector2Int.zero, Building().WithIncome(1000).WithNation(SomeNation).Build());
            var sut = Situation().WithWarFunds(0).WithMap(map).WithNation(SomeNation).Build();

            sut.ManageLogistics();

            sut.WarFunds.Should().Be(1000);
        }

        [Test]
        public void IncomeFromMultipleAllyBuildings()
        {
            var map = new Map(1, 2);
            map.Put(Vector2Int.zero, Building().WithIncome(1500).WithNation(SomeNation).Build());
            map.Put(Vector2Int.up, Building().WithIncome(1000).WithNation(SomeNation).Build());
            var sut = Situation().WithMap(map).WithWarFunds(0).WithNation(SomeNation).Build();

            sut.ManageLogistics();

            sut.WarFunds.Should().Be(2500);
        }
    }
}