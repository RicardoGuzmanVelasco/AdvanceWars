using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class CommandingOfficer
    {
        readonly IList<Maneuver> executedThisTurn = new List<Maneuver>();

        public IEnumerable<Tactic> AvailableTacticsOf([NotNull] Battalion battalion)
        {
            Require(battalion.Equals(Battalion.Null)).False();

            return HasAlready(battalion, Tactic.Wait)
                ? Enumerable.Empty<Tactic>()
                : TacticsOf(battalion).Except(ExecutedThisTurn(battalion));
        }

        bool HasAlready(Battalion battalion, Tactic tactic)
        {
            return ExecutedManeuversOf(battalion).Any(x => x.Is(tactic));
        }

        IEnumerable<Maneuver> ExecutedManeuversOf(Battalion battalion)
        {
            return executedThisTurn.Where(m => m.Performer.Equals(battalion));
        }

        IEnumerable<Tactic> ExecutedThisTurn(Battalion battalion)
        {
            return ExecutedManeuversOf(battalion).Select(x => x.Origin);
        }

        public void Order(Maneuver command)
        {
            executedThisTurn.Add(command);

            if(command.Is(Tactic.Fire))
                executedThisTurn.Add(Maneuver.Wait(command.Performer));
        }

        IEnumerable<Tactic> TacticsOf(Battalion battalion)
        {
            return new List<Tactic>
            {
                Tactic.Wait,
                Tactic.Fire,
                Tactic.Move
            };
        }
    }
}