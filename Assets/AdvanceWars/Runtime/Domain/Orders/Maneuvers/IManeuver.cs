using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public interface IManeuver
    {
        Allegiance Performer { get; }
        Tactic FromTactic { get; }
        bool Is(Tactic tactic);
        void Apply(Map.Map map);
    }
}