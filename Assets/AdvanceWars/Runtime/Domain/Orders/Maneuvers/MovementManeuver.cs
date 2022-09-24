using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public class MovementManeuver : Maneuver
    {
        IEnumerable<Map.Map.Space> Itinerary { get; }

        protected internal MovementManeuver([NotNull] Battalion performer,
            [NotNull] IEnumerable<Map.Map.Space> itinerary)
            : base(performer, Tactic.Move)
        {
            Itinerary = itinerary;
        }

        public override void Apply(Map.Map map)
        {
            map.WhereIs(Performer)!.Unoccupy();
            Itinerary.Last().Enter(Performer);
        }
    }
}