using JetBrains.Annotations;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public abstract class SpawnerManeuver : Maneuver
    {
        protected new Spawner Performer { get; }
        
        protected SpawnerManeuver([NotNull] Spawner performer, Tactic fromTactic) 
            : base(performer, fromTactic)
        {
            Performer = performer;
        }
    }
}