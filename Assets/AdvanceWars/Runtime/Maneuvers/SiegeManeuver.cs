using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public class SiegeManeuver : Maneuver
    {
        public SiegeManeuver([NotNull] Battalion performer) : base(performer, Tactic.Siege) { }

        public override void Apply(Map map)
        {
            map.WhereIs(Performer).Besiege();
        }
    }
}