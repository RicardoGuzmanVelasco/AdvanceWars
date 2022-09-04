using AdvanceWars.Runtime;

namespace AdvanceWars.Tests.Doubles
{
    internal class DummyManeuver : Maneuver
    {
        internal DummyManeuver(Tactic fromTactic, Battalion performer)
            : base(performer, fromTactic) { }

        public override void Apply(Map map) { }
    }
}