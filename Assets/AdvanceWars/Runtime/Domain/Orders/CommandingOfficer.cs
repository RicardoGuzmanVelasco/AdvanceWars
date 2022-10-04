using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders.Maneuvers;
using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;
using static AdvanceWars.Runtime.Domain.Map.Map;
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

        public IEnumerable<Tactic> AvailableTacticsAt([NotNull] Space space)
        {
            Require(space.Something(this)).True();

            if (space.IsOccupied)
            {
                return AvailableBattalionTacticsAt(space);
            }
            else
            {
                return AvailableSpawnerTacticsAt(space);
            }
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
                executedThisTurn.Add(Maneuver.Wait(map.WhereIs(maneuver.Performer as Spawner)!.Occupant));
            
            if(maneuver.Is(Tactic.Fire))
                executedThisTurn.Add(Maneuver.Wait(maneuver.Performer as Battalion));
        }

        private IEnumerable<Tactic> AvailableSpawnerTacticsAt(Space space)
        {
            if (space.SpawnableUnits.Any())
            {
                return new List<Tactic> {Tactic.Recruit}.Except(ExecutedThisTurn(space.Terrain));
            }
            return Enumerable.Empty<Tactic>();
        }
        
        private IEnumerable<Tactic> AvailableBattalionTacticsAt(Space space)
        {
            var battalion = space.Occupant;

            List<Tactic> tactics = new List<Tactic>();

            if (map.WhereIs(battalion)!.HasGuest)
            {
                return new List<Tactic>
                {
                    Tactic.Merge
                };
            }
            
            if(HasAlready(space.Occupant, Tactic.Wait))
                return Enumerable.Empty<Tactic>();

            tactics = new List<Tactic>
            {
                Tactic.Wait
            };

            if (map.RangeOfMovement(battalion).Any())
                tactics.Add(Tactic.Move);

            if (map.EnemyBattalionsInRangeOfFire(battalion).Any(x => battalion.BaseDamageTo(x.Armor) > 0)
                && battalion.AmmoRounds > 0)
                tactics.Add(Tactic.Fire);

            if (map.WhereIs(battalion)!.IsBesiegable)
                tactics.Add(Tactic.Siege);
            
            return tactics.Except(ExecutedThisTurn(space.Occupant));
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