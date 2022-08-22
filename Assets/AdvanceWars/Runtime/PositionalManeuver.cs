using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public class PositionalManeuver : Maneuver
    {
        Map.Space Where { get; }

        public PositionalManeuver([NotNull] Battalion performer, Tactic origin, [NotNull] Map.Space where)
            : base(performer, origin)
        {
            Where = where;
        }
    }
}