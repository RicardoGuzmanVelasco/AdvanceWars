using System.Collections.Generic;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Maneuver : IManeuver
    {
        public Battalion Performer { get; }
        public Tactic Origin { get; }

        #region Ctor/FactoryMethods
        protected Maneuver([NotNull] Battalion performer, Tactic origin)
        {
            Require(performer.Equals(Battalion.Null)).False();
            Performer = performer;
            Origin = origin;
        }

        public static IManeuver Wait([NotNull] Battalion performer)
        {
            return new Maneuver(performer, Tactic.Wait);
        }

        public static IManeuver Fire([NotNull] Battalion performer, [NotNull] Battalion target)
        {
            return new TargetingManeuver(performer, Tactic.Fire, target);
        }

        public static IManeuver Move([NotNull] Battalion battalion, IEnumerable<Map.Space> itinerary)
        {
            return new MovementManeuver(battalion, Tactic.Move, itinerary);
        }
        #endregion

        public bool Is(Tactic tactic)
        {
            return Origin.Equals(tactic);
        }

        public virtual void Apply(Map map) { }
    }
}