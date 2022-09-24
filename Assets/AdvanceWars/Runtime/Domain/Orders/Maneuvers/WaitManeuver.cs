using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    class WaitManeuver : Maneuver
    {
        protected internal WaitManeuver([NotNull] Battalion performer)
            : base(performer, Tactic.Wait) { }

        public override void Apply(Map.Map map)
        {
            //Wait maneuver is just to mark that battalion as non-usable this day.
        }
    }
}