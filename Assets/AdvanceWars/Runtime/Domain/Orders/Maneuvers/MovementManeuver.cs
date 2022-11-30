using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public class MovementManeuver : BattalionManeuver
    {
        IEnumerable<Map.Map.Space> Itinerary { get; }

        protected internal MovementManeuver([NotNull] Battalion performer,
            [NotNull] IEnumerable<Map.Map.Space> itinerary)
            : base(performer, Tactic.Move)
        {
            Itinerary = itinerary;
        }

        public override void Apply(Situation situation)
        {
            situation.WhereIs(Performer)!.Unoccupy();
            Itinerary.Last().Occupy(Performer);
        }
    }
}