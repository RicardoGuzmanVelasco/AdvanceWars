using System.Collections.Generic;
using AdvanceWars.Runtime;

namespace AdvanceWars.Tests.Builders
{
    internal class CommandingOfficerBuilder
    {
        readonly Map map = default;
        Nation nation = default;

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
            this.nation = motherland;

            return this;
        }

        public CommandingOfficer Build()
        {
            return new CommandingOfficer(from: nation, map);
        }
    }
}