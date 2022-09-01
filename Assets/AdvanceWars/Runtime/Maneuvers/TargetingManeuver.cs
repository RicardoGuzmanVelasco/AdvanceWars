using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public abstract class TargetingManeuver : Maneuver
    {
        protected Battalion Target { get; }

        protected TargetingManeuver
        (
            [NotNull] Battalion performer,
            Tactic origin,
            [NotNull] Battalion target
        ) : base(performer, origin)
        {
            Target = target;
        }
    }
}