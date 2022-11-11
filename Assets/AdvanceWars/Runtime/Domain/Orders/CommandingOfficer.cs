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
    public class CommandingOfficer 
    {
        readonly IList<IManeuver> executedThisTurn = new List<IManeuver>();
        readonly Situation situation;
        
        public CommandingOfficer(Situation situation)
        {
            this.situation = situation;
        }

        public Nation Motherland => situation.Motherland;

        public IEnumerable<Tactic> AvailableTacticsAt([NotNull] Space space)
        {
            Require(space.ExclusivePresenceOfAlliesTo(situation)).True();

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
            Require(maneuver.Performer.IsAlly(situation)).True();

            maneuver.Apply(situation);
            executedThisTurn.Add(maneuver);

            if (maneuver.Is(Tactic.Recruit))
                executedThisTurn.Add(Maneuver.Wait(situation.WhereIs(maneuver.Performer as Spawner)!.Occupant));
            
            if(maneuver.Is(Tactic.Fire))
                executedThisTurn.Add(Maneuver.Wait(maneuver.Performer as Battalion));
        }

        private IEnumerable<Tactic> AvailableSpawnerTacticsAt(Space space)
        {
            if (space.SpawnableUnits.Any(x => situation.CanAfford(x)))
            {
                return new List<Tactic> {Tactic.Recruit}.Except(ExecutedThisTurn(space.Terrain));
            }
            return Enumerable.Empty<Tactic>();
        }
        
        private IEnumerable<Tactic> AvailableBattalionTacticsAt(Space space)
        {
            var battalion = space.Occupant;

            List<Tactic> tactics = new List<Tactic>();

            if (situation.WhereIs(battalion)!.HasGuest)
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

            if (situation.RangeOfMovement(battalion).Any())
                tactics.Add(Tactic.Move);

            if (situation.EnemyBattalionsInRangeOfFire(battalion).Any(x => battalion.BaseDamageTo(x.Armor) > 0)
                && battalion.AmmoRounds > 0)
                tactics.Add(Tactic.Fire);

            if (situation.WhereIs(battalion)!.IsBesiegable)
                tactics.Add(Tactic.Siege);
            
            return tactics.Except(ExecutedThisTurn(space.Occupant));
        }

        public void BeginTurn()
        {
            executedThisTurn.Clear();
            
            //maniobras automáticas. Sacar el clear al EndTurn.
            situation.ManageLogistics();
        }

        public override string ToString()
        {
            return $"from {situation.Motherland}";
        }
    }
}