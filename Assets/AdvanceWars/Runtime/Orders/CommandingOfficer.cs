using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class CommandingOfficer : Allegiance
    {
        readonly Map map;
        readonly IList<IManeuver> executedThisTurn = new List<IManeuver>();

        public CommandingOfficer(Nation from, Map map)
        {
            this.Motherland = from;
            this.map = map;
        }

        public IEnumerable<Tactic> AvailableTacticsOf([NotNull] Battalion battalion)
        {
            Require(battalion is INull).False();
            Require(battalion.IsAlly(this)).True();
            
            if(HasAlready(battalion, Tactic.Wait))
                return Enumerable.Empty<Tactic>();

            return TacticsOf(battalion).Except(ExecutedThisTurn(battalion));
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
            return ExecutedManeuversOf(battalion).Select(x => x.FromTactic);
        }

        public void Order(IManeuver maneuver)
        {
            Require(maneuver.Performer.IsAlly(this)).True();

            maneuver.Apply(map);
            executedThisTurn.Add(maneuver);

            if(maneuver.Is(Tactic.Fire))
                executedThisTurn.Add(Maneuver.Wait(maneuver.Performer));
        }

        IEnumerable<Tactic> TacticsOf(Battalion battalion)
        {
            var tactics = new List<Tactic>
            {
                Tactic.Wait,
                Tactic.Move
            };

            var enemiesInRange = map.EnemyBattalionsInRangeOfFire(battalion).ToArray();

            if(enemiesInRange.Any())
            {
                if (enemiesInRange!.Any(x => battalion.BaseDamageTo(x.Armor) > 0))
                {
                    tactics.Add(Tactic.Fire);
                }
            }

            if(map.WhereIs(battalion)!.IsBesiegable)
                tactics.Add(Tactic.Siege);

            return tactics;
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