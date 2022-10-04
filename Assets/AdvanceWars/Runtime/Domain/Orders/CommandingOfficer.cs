using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders.Maneuvers;
using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Orders
{
    public class CommandingOfficer : Allegiance
    {
        readonly Map.Map map;
        readonly IList<IManeuver> executedThisTurn = new List<IManeuver>();
 
        public CommandingOfficer(Nation from, Map.Map map)
        {
            this.Motherland = from;
            this.map = map;
        }

        public IEnumerable<Tactic> AvailableTacticsOf([NotNull] Allegiance allegiance)
        {
            Require(allegiance is INull).False();
            Require(allegiance.IsAlly(this)).True();

            if(HasAlready(allegiance, Tactic.Wait))
                return Enumerable.Empty<Tactic>();

            return TacticsOf(allegiance).Except(ExecutedThisTurn(allegiance));
        }

        bool HasAlready(Allegiance allegiance, Tactic tactic)
        {
            return ExecutedManeuversOf(allegiance).Any(x => x.Is(tactic));
        }

        IEnumerable<IManeuver> ExecutedManeuversOf(Allegiance allegiance)
        {
            return executedThisTurn.Where(m => m.Performer.Equals(allegiance));
        }

        IEnumerable<Tactic> ExecutedThisTurn(Allegiance allegiance)
        {
            return ExecutedManeuversOf(allegiance).Select(x => x.FromTactic);
        }

        public void Order(IManeuver maneuver)
        {
            Require(maneuver.Performer.IsAlly(this)).True();

            maneuver.Apply(map);
            executedThisTurn.Add(maneuver);

            if (maneuver.Is(Tactic.Recruit))
                executedThisTurn.Add(Maneuver.Wait(map.WhereIs(maneuver.Spawner).Occupant));
            
            if(maneuver.Is(Tactic.Fire))
                executedThisTurn.Add(Maneuver.Wait(maneuver.Battalion));
        }

        IEnumerable<Tactic> TacticsOf(Allegiance allegiance)
        {
            if (allegiance is Battalion)
            {
                var battalion = allegiance as Battalion;
                if (map.WhereIs(battalion)!.Guest == battalion)
                {
                    return new List<Tactic>
                    {
                        Tactic.Merge
                    };
                }

                var battalionTactics = new List<Tactic>
                {
                    Tactic.Wait
                };

                if (map.RangeOfMovement(battalion).Any())
                    battalionTactics.Add(Tactic.Move);

                if (map.EnemyBattalionsInRangeOfFire(battalion).Any(x => battalion.BaseDamageTo(x.Armor) > 0)
                    && battalion.AmmoRounds > 0)
                    battalionTactics.Add(Tactic.Fire);

                if (map.WhereIs(battalion)!.IsBesiegable)
                    battalionTactics.Add(Tactic.Siege);

                return battalionTactics;
            }

            return new List<Tactic>
            {
                Tactic.Recruit
            };
        }

        public void BeginTurn()
        {
            executedThisTurn.Clear();
            //maniobras automáticas. Sacar el clear al EndTurn.

            foreach (var space in map.AllySpaces(this))
            {
                space.HealOccupant();
                space.ReplenishOccupantAmmo();
            }
        }

        public override string ToString()
        {
            return $"from {Motherland}";
        }
    }
}