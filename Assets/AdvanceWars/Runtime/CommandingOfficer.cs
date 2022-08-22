using System.Collections.Generic;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class CommandingOfficer
    {
        public IEnumerable<Tactic> AvailableTacticsOf([NotNull] Battalion batallion)
        {
            Require(batallion.Equals(Battalion.Null)).False();
            yield return new Tactic("Wait");
        }

        public void Order(Maneuver command)
        {
            Maneuvers.Add(command);
        }

        public IList<Maneuver> Maneuvers { get; } = new List<Maneuver>();
    }
}