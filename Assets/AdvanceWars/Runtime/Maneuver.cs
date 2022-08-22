using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Maneuver
    {
        public Battalion Performer { get; }
        public Tactic Origin { get; }

        #region Ctor/FactoryMethods
        protected Maneuver([NotNull] Battalion performer, Tactic origin)
        {
            Require(performer.Equals(Battalion.Null)).False();
            this.Origin = origin;
            Performer = performer;
        }

        public static Maneuver Wait([NotNull] Battalion performer)
        {
            return new Maneuver(performer, Tactic.Wait);
        }

        public static Maneuver Fire([NotNull] Battalion performer, [NotNull] Battalion target)
        {
            return new TargetingManeuver(performer, Tactic.Fire, target);
        }

        public static Maneuver Move([NotNull] Battalion battalion, Map.Space space)
        {
            return new PositionalManeuver(battalion, Tactic.Move, space);
        }
        #endregion

        public bool Is(Tactic tactic)
        {
            return Origin.Equals(tactic);
        }
    }
}