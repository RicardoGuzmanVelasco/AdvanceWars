using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public class SiegeManeuver : Maneuver
    {
        public SiegeManeuver([NotNull] Battalion performer)
            : base(performer, Tactic.Siege) { }

        public override void Apply(Map.Map map)
        {
            Require(map.WhereIs(Battalion)).Not.Null();
            map.WhereIs(Battalion)!.Besiege();
        }
    }
}