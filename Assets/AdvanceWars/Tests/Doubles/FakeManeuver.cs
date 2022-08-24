using AdvanceWars.Runtime;
using JetBrains.Annotations;

namespace AdvanceWars.Tests.Doubles
{
    public static class FakeManeuverBuilder
    {
        public static Maneuver FakeFire([NotNull] Battalion performer)
        {
            return new FakeManeuver(performer, Tactic.Fire);
        }

        public static Maneuver FakeMove([NotNull] Battalion performer)
        {
            return new FakeManeuver(performer, Tactic.Move);
        }

        class FakeManeuver : Maneuver
        {
            public FakeManeuver(Battalion performer, Tactic origin)
                : base(performer, origin) { }

            public override void Apply(Map map) { }
        }
    }
}