namespace AdvanceWars.Runtime
{
    public interface IManeuver
    {
        Battalion Performer { get; }
        Tactic FromTactic { get; }
        bool Is(Tactic tactic);
        void Apply(Map map);
    }
}