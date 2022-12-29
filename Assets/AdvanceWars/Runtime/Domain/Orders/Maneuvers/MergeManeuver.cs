using AdvanceWars.Runtime.Domain.Troops;
using AdvanceWars.Runtime.Extensions.DataStructures;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public class MergeManeuver : TargetingManeuver
    {
        readonly Treasury treasury;

        public MergeManeuver([NotNull] Battalion performer, Battalion target, Treasury treasury)
            : base(performer, Tactic.Merge, target)
        {
            this.treasury = treasury;
        }

        public override void Apply(Situation situation)
        {
            var amount = OverflownForces() * Target.PricePerSoldier;
            if(amount > 0)
            {
                treasury.Earn(amount);
            }

            Target.Heal(Performer.Forces);

            situation.WhereIs(Performer).Unoccupy();
        }

        int OverflownForces() => Performer.Forces.Value + Target.Forces.Value - Battalion.MaxForces;
    }
}