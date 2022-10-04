using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public abstract class BattalionManeuver : Maneuver
    {
        protected new Battalion Performer { get; }
        
        protected BattalionManeuver([NotNull] Battalion performer, Tactic fromTactic) 
            : base(performer, fromTactic)
        {
            Performer = performer;
        }
    }
}