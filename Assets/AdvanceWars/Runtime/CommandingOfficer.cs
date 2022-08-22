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

            if(executedThisTurn.Any(x => x.Performer.Equals(batallion) && x.Origin.Equals(Tactic.Wait())))
            {
                return Enumerable.Empty<Tactic>();
            }

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

        private IEnumerable<Tactic> TacticsOf(Battalion battalion)
        {
            return new List<Tactic> { Tactic.Wait(), Tactic.Fire() };
        }
    }
}