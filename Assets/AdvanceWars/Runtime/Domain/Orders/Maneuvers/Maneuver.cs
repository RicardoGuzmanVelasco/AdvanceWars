using System.Collections.Generic;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public abstract class Maneuver : IManeuver
    {
        public Battalion Performer { get; }
        public Tactic FromTactic { get; }

        #region Ctor/FactoryMethods
        protected Maneuver([NotNull] Battalion performer, Tactic fromTactic)
        {
            Require(performer is INull).False();
            Performer = performer;
            FromTactic = fromTactic;
        }

        public static IManeuver Wait([NotNull] Battalion performer)
        {
            return new WaitManeuver(performer);
        }

        public static IManeuver Fire([NotNull] Battalion performer, [NotNull] Battalion target)
        {
            return new FireManeuver(performer, target);
        }

        public static IManeuver Move([NotNull] Battalion battalion, IEnumerable<Map.Space> itinerary)
        {
            return new MovementManeuver(battalion, itinerary);
        }

        public static IManeuver Siege([NotNull] Battalion battalion)
        {
            return new SiegeManeuver(battalion);
        }

        public static IManeuver Merge([NotNull] Battalion battalion)
        {
            return new MergeManeuver(battalion);
        }
        #endregion

        public bool Is(Tactic tactic)
        {
            return FromTactic.Equals(tactic);
        }

        public abstract void Apply(Map map);
    }
}