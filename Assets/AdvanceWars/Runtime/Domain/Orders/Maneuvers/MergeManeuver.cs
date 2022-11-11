using System;
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
            //(Unit Price / 10) x (PlatoonsA + PlatoonsB - 10)
            //Ex: Infantry 9 Platoons Join 9 Platoons.  (price 1000) : 1000 / 10 x (9 + 9 - 10) = 800
            int extraPlatoons = Performer.Platoons + battalion.Platoons - Battalion.MaxPlatoons;
            if (extraPlatoons > 0)
            {
                treasury.Earn(extraPlatoons * battalion.Price / 10);
            }
        }
    }
}