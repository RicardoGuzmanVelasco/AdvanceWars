using System.Collections.Generic;
using AdvanceWars.Runtime;

namespace AdvanceWars.Tests.Builders
{
    internal class CommandingOfficerBuilder
    {
        Map map = Map.Null;
        Nation nation;

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

        public CommandingOfficerBuilder WithMap(Map map)
        {
            this.map = map;

            return this;
        }

        public CommandingOfficer Build()
        {
            return new CommandingOfficer(from: nation, map);
        }
    }
}