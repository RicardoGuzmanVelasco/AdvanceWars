using System;
using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public class MergeManeuver : Maneuver
    {
        public MergeManeuver([NotNull] Battalion performer) : base(performer, Tactic.Merge) { }

        public override void Apply(Map.Map map)
        {
            var performerSpace = map.WhereIs(Battalion);
            var occupant = performerSpace.Occupant;
            var totalForces = performerSpace.Occupant.Forces + Battalion.Forces;

            occupant.Forces = Math.Clamp(totalForces, 0, Battalion.MaxForces);
            performerSpace.ExpelGuest();
        }
    }
}