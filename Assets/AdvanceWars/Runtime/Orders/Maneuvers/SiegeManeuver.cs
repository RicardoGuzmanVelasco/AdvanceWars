using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class SiegeManeuver : Maneuver
    {
        public SiegeManeuver([NotNull] Battalion performer)
            : base(performer, Tactic.Siege) { }

        public override void Apply(Map map)
        {
            Require(map.WhereIs(Performer)).Not.Null();
            map.WhereIs(Performer)!.Besiege();
        }
    }
}