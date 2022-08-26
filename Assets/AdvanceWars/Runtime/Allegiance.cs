using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public abstract class Allegiance
    {
        public Nation Motherland { get; init; }
        public virtual bool IsEnemy(Allegiance other) => !IsAlly(other);

        public virtual bool IsAlly(Allegiance other)
        {
            return RelationshipWith(other) is DiplomaticRelation.Ally;
        }

        [Pure]
        public DiplomaticRelation RelationshipWith([NotNull] Allegiance other)
        {
            if(other.Motherland.IsStateless || Motherland.IsStateless)
                return DiplomaticRelation.Neutral;
            if(other.Motherland.Equals(Motherland))
                return DiplomaticRelation.Ally;
            else
                return DiplomaticRelation.Enemy;
        }
    }
}