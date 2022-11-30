using System.Collections.Generic;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Troops;
using static AdvanceWars.Tests.Builders.SituationBuilder;

namespace AdvanceWars.Tests.Builders
{
    internal class CommandingOfficerBuilder
    {
        SituationBuilder situation = Situation();

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
        public CommandingOfficerBuilder Ally() => WithNation("IsAlly");
        public CommandingOfficerBuilder Enemy() => WithNation("IsEnemy");
        
        public CommandingOfficerBuilder WithNation(Nation motherland)
        {
            situation.WithNation(motherland);

            return this;
        }

        public CommandingOfficerBuilder WithWarFunds(int amount)
        {
            situation.WithWarFunds(amount);
            return this;
        }

        public CommandingOfficerBuilder WithTreasury(Treasury treasury)
        {
            situation.WithTreasury(treasury);
            return this;
        }

        public CommandingOfficerBuilder WithMap(Map map)
        {
            situation.WithMap(map);

            return this;
        }

        public CommandingOfficerBuilder WithNation(string nation)
        {
            situation.WithNation(nation);

            return this;
        }

        public CommandingOfficer Build()
        {
            return new CommandingOfficer(situation.Build());
        }
    }
}