﻿using AdvanceWars.Runtime.Domain.Fire;
using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public class FireManeuver : TargetingManeuver
    {
        protected internal FireManeuver([NotNull] Battalion performer, [NotNull] Battalion target)
            : base(performer, Tactic.Fire, target) { }

        public override void Apply(Map.Map map)
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