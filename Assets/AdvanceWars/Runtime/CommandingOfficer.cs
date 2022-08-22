using System.Collections.Generic;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class CommandingOfficer
    {
        IList<Maneuver> executedThisTurn = new List<Maneuver>();
        Dictionary<Battalion, List<Tactic>> tactics = new Dictionary<Battalion, List<Tactic>>();

        public IEnumerable<Tactic> AvailableTacticsOf([NotNull] Battalion batallion)
        {
            Require(batallion.Equals(Battalion.Null)).False();

            if(!tactics.ContainsKey(batallion))
                tactics[batallion] = new List<Tactic> { Tactic.Wait() };

            return tactics[batallion];
        }

        public void Order(Maneuver command)
        {
            if(!tactics.ContainsKey(command.Performer))
                tactics[command.Performer] = new List<Tactic>() { Tactic.Wait() };

            executedThisTurn.Add(command);
            tactics[command.Performer].Remove(command.Origin);
        }

        private IEnumerable<Tactic> TacticsOf(Battalion batallion)
        {
            return new List<Tactic> { Tactic.Wait(), Tactic.Fire() };
        }

        public IEnumerable<Tactic> Something(Battalion battalion)
        {
            return new List<Tactic> { Tactic.Wait() };
        }
    }
}