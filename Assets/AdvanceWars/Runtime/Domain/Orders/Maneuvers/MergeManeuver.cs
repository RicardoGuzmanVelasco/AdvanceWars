using System;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public class MergeManeuver : Maneuver
    {
        public MergeManeuver([NotNull] Battalion performer) : base(performer, Tactic.Merge) { }

        public override void Apply(Map map)
        {
            var performerSpace = map.WhereIs(Performer);
            var occupant = performerSpace.Occupant;
            var totalForces = performerSpace.Occupant.Forces + Performer.Forces;

            occupant.Forces = Math.Clamp(totalForces, 0, Battalion.MaxForces);
            performerSpace.ExpelGuest();
        }
    }
}