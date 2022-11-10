using System.Collections.Generic;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Tests.Builders
{
    internal class CommandingOfficerBuilder
    {
        Map map = Map.Null;
        Nation nation;
        int warFunds = 0;

        #region ObjectMothers
        public static CommandingOfficerBuilder CommandingOfficer() => new CommandingOfficerBuilder();

        public static IList<CommandingOfficer> CommandingOfficers(int count)
        {
            var result = new List<CommandingOfficer>();

            for(var i = 0; i < count; i++)
                result.Add(CommandingOfficer().Build());

            return result;
        }
        #endregion

        public CommandingOfficerBuilder Of(Nation motherland)
        {
            nation = motherland;

            return this;
        }

        public CommandingOfficerBuilder WithWarFunds(int amount)
        {
            warFunds = amount;
            return this;
        }
        
        public CommandingOfficerBuilder WithMap(Map map)
        {
            this.map = map;

            return this;
        }

        public CommandingOfficerBuilder WithNation(string nation)
        {
            this.nation = new Nation(nation);

            return this;
        }
        
        public CommandingOfficerBuilder WithNation(Nation nation)
        {
            this.nation = nation;

            return this;
        }

        public CommandingOfficer Build()
        {
            return new CommandingOfficer(from: nation, map, new Treasury(0));
        }
    }
}