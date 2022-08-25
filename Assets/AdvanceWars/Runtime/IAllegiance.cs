using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public interface IAllegiance
    {
        Nation Motherland { get; }
        bool IsAlly([NotNull] IAllegiance other);
        bool IsEnemy([NotNull] IAllegiance other);
    }
}