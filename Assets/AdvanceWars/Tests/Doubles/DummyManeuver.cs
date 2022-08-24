using AdvanceWars.Runtime;

namespace AdvanceWars.Tests.Doubles
{
    internal class DummyManeuver : Maneuver
    {
        internal DummyManeuver(Tactic origin, Battalion performer)
            : base(performer, origin) { }

        public override void Apply(Map map) { }
    }
}