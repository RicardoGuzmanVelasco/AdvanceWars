using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public class MovementManeuver : Maneuver
    {
        IEnumerable<Map.Space> Itinerary { get; }

        public MovementManeuver([NotNull] Battalion performer, Tactic origin,
            [NotNull] IEnumerable<Map.Space> itinerary)
            : base(performer, origin)
        {
            Itinerary = itinerary;
        }

        public override void Apply(Map map)
        {
            map.WhereIs(Performer)!.Occupant = Battalion.Null;
            Itinerary.Last().Occupant = Performer;
        }
    }
}