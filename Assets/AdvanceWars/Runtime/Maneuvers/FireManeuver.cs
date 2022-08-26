using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public class FireManeuver : TargetingManeuver
    {
        protected internal FireManeuver([NotNull] Battalion performer, [NotNull] Battalion target)
            : base(performer, Tactic.Fire, target) { }
    }
}