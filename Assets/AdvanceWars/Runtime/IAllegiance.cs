using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public interface IAllegiance
    {
        Nation Motherland { get; }
        bool IsFriend([NotNull] IAllegiance other);
        bool IsEnemy([NotNull] IAllegiance other);
    }
}