using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class CommandingOfficer
    {
        readonly IList<Maneuver> executedThisTurn = new List<Maneuver>();

        public IEnumerable<Tactic> AvailableTacticsOf([NotNull] Battalion batallion)
        {
            Require(batallion.Equals(Battalion.Null)).False();
            return TacticsOf(batallion).Except(UsedThisTurn(batallion));
        }

        IEnumerable<Tactic> UsedThisTurn(Battalion batallion)
        {
            return executedThisTurn.Where(x => x.Performer.Equals(batallion)).Select(x => x.Origin);
        }

        public void Order(Maneuver command)
        {
            executedThisTurn.Add(command);
        }

        private IEnumerable<Tactic> TacticsOf(Battalion batallion)
        {
            return new List<Tactic> { Tactic.Wait() };
        }
    }
}