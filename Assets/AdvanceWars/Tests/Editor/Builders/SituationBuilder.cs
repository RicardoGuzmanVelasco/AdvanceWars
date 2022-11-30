using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Tests.Builders
{
    internal class SituationBuilder
    {
        Treasury treasury = new Treasury(0);
        Map map = Map.Null;
        Nation nation = Nation.Stateless;

        public static SituationBuilder Situation() => new SituationBuilder();

        public SituationBuilder WithWarFunds(int amount)
        {
            treasury = new Treasury(amount);
            return this;
        }

        public SituationBuilder WithTreasury(Treasury treasury)
        {
            this.treasury = treasury;
            return this;
        }

        public SituationBuilder WithNation(Nation nation)
        {
            this.nation = nation;
            return this;
        }

        public SituationBuilder WithNation(string nation)
        {
            this.nation = new Nation(nation);
            return this;
        }

        public SituationBuilder WithMap(Map map)
        {
            this.map = map;

            return this;
        }

        public Situation Build()
        {
            return new Situation
            {
                Map = map,
                Treasury = treasury,
                Motherland = nation
            };
        }
    }
}