using System.Collections.Generic;

namespace AdvanceWars.Runtime
{
    public class CommandingOfficer
    {
        public IEnumerable<Tactic> AvailableTacticsOf(Battalion batallion)
        {
            yield return new Tactic();
        }

        public void Order(Maneuver command)
        {
            Maneuvers.Add(command);
        }

        public IList<Maneuver> Maneuvers { get; } = new List<Maneuver>();
    }
}