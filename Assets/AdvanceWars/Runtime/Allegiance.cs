using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public abstract class Allegiance
    {
        public Nation Motherland { get; init; }
        public bool IsEnemy(Allegiance other) => RelationshipWith(other) is DiplomaticRelation.Enemy;

        public bool IsAlly(Allegiance other) => RelationshipWith(other) is DiplomaticRelation.Ally;

        public bool IsNeutral(Allegiance other) => RelationshipWith(other) is DiplomaticRelation.Neutral;

        [Pure]
        DiplomaticRelation RelationshipWith([NotNull] Allegiance other)
        {
            if(other.Motherland.Equals(Nation.Stateless) || Motherland.Equals(Nation.Stateless))
                return DiplomaticRelation.Neutral;
            if(other.Motherland.Equals(Motherland))
                return DiplomaticRelation.Ally;
            else
                return DiplomaticRelation.Enemy;
        }

        enum DiplomaticRelation
        {
            Neutral,
            Ally,
            Enemy
        }
    }
}