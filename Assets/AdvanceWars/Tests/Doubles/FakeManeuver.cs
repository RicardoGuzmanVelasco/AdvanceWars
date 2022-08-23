using AdvanceWars.Runtime;
using JetBrains.Annotations;

namespace AdvanceWars.Tests.Doubles
{
    public class FakeManeuver : Maneuver
    {
        FakeManeuver(Battalion performer, Tactic origin)
            : base(performer, origin) { }

        public static FakeManeuver Fire([NotNull] Battalion performer)
        {
            return new FakeManeuver(performer, Tactic.Fire);
        }

        public static FakeManeuver Move([NotNull] Battalion performer)
        {
            return new FakeManeuver(performer, Tactic.Move);
        }
    }
}