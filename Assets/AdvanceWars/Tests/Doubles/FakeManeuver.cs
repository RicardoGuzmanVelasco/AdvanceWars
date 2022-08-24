using AdvanceWars.Runtime;
using JetBrains.Annotations;

namespace AdvanceWars.Tests.Doubles
{
    public static class FakeManeuverBuilder
    {
        public static IManeuver FakeFire([NotNull] Battalion performer)
        {
            return new FakeManeuver(performer, Tactic.Fire);
        }

        public static IManeuver FakeMove([NotNull] Battalion performer)
        {
            return new FakeManeuver(performer, Tactic.Move);
        }

        class FakeManeuver : Maneuver
        {
            public FakeManeuver(Battalion performer, Tactic origin)
                : base(performer, origin) { }
        }
    }
}