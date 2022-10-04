using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public class SiegeManeuver : BattalionManeuver
    {
        public SiegeManeuver([NotNull] Battalion performer)
            : base(performer, Tactic.Siege) { }

        public override void Apply(Map.Map map)
        {
            Require(map.WhereIs(Performer)).Not.Null();
            map.WhereIs(Performer)!.Besiege();
        }
    }
}