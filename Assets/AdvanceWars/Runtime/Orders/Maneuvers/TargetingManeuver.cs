using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public abstract class TargetingManeuver : Maneuver
    {
        protected Battalion Target { get; }

        protected TargetingManeuver
        (
            [NotNull] Battalion performer,
            Tactic fromTactic,
            [NotNull] Battalion target
        ) : base(performer, fromTactic)
        {
            Target = target;
        }
    }
}