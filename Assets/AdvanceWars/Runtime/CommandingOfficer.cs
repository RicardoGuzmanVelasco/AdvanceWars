using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class CommandingOfficer : IAllegiance
    {
        readonly Map map;
        readonly IList<IManeuver> executedThisTurn = new List<IManeuver>();

        public Nation Motherland { get; }

        public CommandingOfficer(Nation from, Map map)
        {
            this.Motherland = from;
            this.map = map;
        }

        public bool IsAlly(IAllegiance other)
        {
            throw new System.NotImplementedException();
        }

        public bool IsEnemy(IAllegiance other)
        {
            throw new System.NotImplementedException();
        }

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

        IEnumerable<IManeuver> ExecutedManeuversOf(Battalion battalion)
        {
            return executedThisTurn.Where(m => m.Performer.Equals(battalion));
        }

        IEnumerable<Tactic> ExecutedThisTurn(Battalion battalion)
        {
            return ExecutedManeuversOf(battalion).Select(x => x.Origin);
        }

        public void Order(IManeuver maneuver)
        {
            maneuver.Apply(map);
            executedThisTurn.Add(maneuver);

            if(maneuver.Is(Tactic.Fire))
                executedThisTurn.Add(Maneuver.Wait(maneuver.Performer));
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

        public void BeginTurn()
        {
            executedThisTurn.Clear();
            //maniobras automáticas. Sacar el clear al EndTurn.
        }

        public override string ToString()
        {
            return $"from {Motherland}";
        }
    }
}