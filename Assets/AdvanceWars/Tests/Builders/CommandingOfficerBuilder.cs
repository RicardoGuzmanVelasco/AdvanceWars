using AdvanceWars.Runtime;

namespace AdvanceWars.Tests.Builders
{
    internal class CommandingOfficerBuilder
    {
        readonly Map map = default;
        Nation nation = default;

        #region ObjectMothers
        public static CommandingOfficerBuilder CommandingOfficer() => new CommandingOfficerBuilder();
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