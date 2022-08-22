using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public class TargetingManeuver : Maneuver
    {
        Battalion Target { get; }

        public TargetingManeuver([NotNull] Battalion performer, Tactic origin, [NotNull] Battalion target)
            : base(performer, origin)
        {
            Target = target;
        }
    }
}