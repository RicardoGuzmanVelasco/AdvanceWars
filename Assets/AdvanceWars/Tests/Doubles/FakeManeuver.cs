using AdvanceWars.Runtime;

namespace AdvanceWars.Tests.Doubles
{
    internal class FakeManeuver : Maneuver
    {
        internal FakeManeuver(Tactic origin, Battalion performer)
            : base(performer, origin) { }

        public override void Apply(Map map) { }
    }
}