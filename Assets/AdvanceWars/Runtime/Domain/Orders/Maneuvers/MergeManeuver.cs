using System;
using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public class MergeManeuver : BattalionManeuver
    {
        public MergeManeuver([NotNull] Battalion performer) : base(performer, Tactic.Merge) { }

        public override void Apply(Map.Map map)
        {
            var performerSpace = map.WhereIs(Performer);
            var occupant = performerSpace.Occupant;

            occupant.Heal(Performer.Forces);
            performerSpace.ExpelGuest();
        }
    }
}