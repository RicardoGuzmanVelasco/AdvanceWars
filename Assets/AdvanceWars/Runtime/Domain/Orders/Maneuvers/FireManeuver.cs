using AdvanceWars.Runtime.Domain.Fire;
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
            var performerSpace = map.WhereIs(Battalion);
            var targetSpace = map.WhereIs(Target);

            var combat = new Combat
            (
                new TheaterOps(performerSpace!.Terrain, Battalion),
                new TheaterOps(targetSpace!.Terrain, Target)
            );

            var outcome = combat.PredictOutcome();

            performerSpace.ReportCasualties(outcome.Atk.Forces);
            performerSpace.ConsumeAmmoRound();
            
            targetSpace.ReportCasualties(outcome.Def.Forces);
            targetSpace.ConsumeAmmoRound();
        }
    }
}