﻿using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public abstract class TargetingManeuver : Maneuver
    {
        Battalion Target { get; }

        public TargetingManeuver([NotNull] Battalion performer, Tactic origin, [NotNull] Battalion target)
            : base(performer, origin)
        {
            Target = target;
        }

        public override void Apply(Map map)
        {
            var performerSpace = map.WhereIs(Performer);
            var targetSpace = map.WhereIs(Target);

            var combat = new Combat
            (
                new TheaterOps(performerSpace!.Terrain, Performer),
                new TheaterOps(targetSpace!.Terrain, Target)
            );

            var outcome = combat.PredictOutcome();

            performerSpace.ReportCasualties(outcome.Atk.Forces);
            targetSpace.ReportCasualties(outcome.Def.Forces);
        }
    }
}