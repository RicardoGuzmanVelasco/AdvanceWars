using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Orders.Maneuvers;
using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Tests.Doubles
{
    internal class DummyManeuver : Maneuver
    {
        internal DummyManeuver(Tactic fromTactic, Battalion performer)
            : base(performer, fromTactic) { }

        public override void Apply(Map map) { }
    }
}