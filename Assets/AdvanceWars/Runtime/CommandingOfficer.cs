using System.Collections.Generic;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class CommandingOfficer
    {
        public IList<Maneuver> Maneuvers { get; } = new List<Maneuver>();
        Dictionary<Battalion, List<Tactic>> tactics = new Dictionary<Battalion, List<Tactic>>();

        public IEnumerable<Tactic> AvailableTacticsOf([NotNull] Battalion batallion)
        {
            Require(batallion.Equals(Battalion.Null)).False();

            var defaultWait = new Tactic("Wait");

            if(!tactics.ContainsKey(batallion))
                tactics[batallion] = new List<Tactic> { defaultWait };

            return tactics[batallion];
        }

        public void Order(Maneuver command)
        {
            tactics[command.Performer] = new List<Tactic>();
            Maneuvers.Add(command);
        }
    }
}