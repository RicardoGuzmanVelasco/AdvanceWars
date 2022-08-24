using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public class MovementManeuver : Maneuver
    {
        IEnumerable<Map.Space> Itinerary { get; }

        protected internal MovementManeuver([NotNull] Battalion performer, [NotNull] IEnumerable<Map.Space> itinerary)
            : base(performer, Tactic.Move)
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