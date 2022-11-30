using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public class MergeManeuver : TargetingManeuver
    {
        readonly Treasury treasury;

        public MergeManeuver([NotNull] Battalion performer, Battalion target, Treasury treasury) : base(performer, Tactic.Merge, target)
        {
            this.treasury = treasury;
        }

        public override void Apply(Situation situation)
        {
            var performerSpace = situation.WhereIs(Performer);
            
            var extraForces = Performer.Forces + Target.Forces - Battalion.MaxForces;
            if(extraForces > 0)
            {
                treasury.Earn(extraForces * Target.PricePerSoldier);
            }

            Target.Heal(Performer.Forces);
            
            performerSpace.Unoccupy();
        }
    }
}