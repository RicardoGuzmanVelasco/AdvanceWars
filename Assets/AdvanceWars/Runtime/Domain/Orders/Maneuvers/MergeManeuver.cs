using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public class MergeManeuver : BattalionManeuver
    {
        readonly Treasury treasury;

        public MergeManeuver([NotNull] Battalion performer, Treasury treasury) : base(performer, Tactic.Merge)
        {
            this.treasury = treasury;
        }

        public override void Apply(Situation situation)
        {
            var performerSpace = situation.WhereIs(Performer);
            var occupant = performerSpace.Occupant;

            RecoupFunds(occupant);

            occupant.Heal(Performer.Forces);
            performerSpace.ExpelGuest();
        }

        void RecoupFunds(Battalion battalion)
        {
            var extraForces = Performer.Forces + battalion.Forces - Battalion.MaxForces;
            if(extraForces > 0)
            {
                treasury.Earn(extraForces * battalion.PricePerSoldier);
            }
        }
    }
}