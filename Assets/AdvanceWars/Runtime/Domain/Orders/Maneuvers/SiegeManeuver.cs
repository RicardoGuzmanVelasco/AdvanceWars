using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public class SiegeManeuver : BattalionManeuver
    {
        public SiegeManeuver([NotNull] Battalion performer)
            : base(performer, Tactic.Siege) { }

        public override void Apply(Situation situation)
        {
            Require(situation.WhereIs(Performer)).Not.Null();
            situation.WhereIs(Performer)!.Besiege();
        }
    }
}