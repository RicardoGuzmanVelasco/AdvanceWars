namespace AdvanceWars.Runtime
{
    public interface IManeuver
    {
        Battalion Performer { get; }
        Tactic Origin { get; }
        bool Is(Tactic tactic);
        void Apply(Map map);
    }
}