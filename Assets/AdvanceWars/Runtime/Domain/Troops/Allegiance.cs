using JetBrains.Annotations;

namespace AdvanceWars.Runtime.Domain.Troops
{
    public abstract class Allegiance
    {
        enum DiplomaticRelation
        {
            Neutral,
            Ally,
            Enemy
        }

        public Nation Motherland { get; init; }

        public bool IsEnemy(Allegiance other) => RelationshipWith(other) is DiplomaticRelation.Enemy;
        public bool IsAlly(Allegiance other) => RelationshipWith(other) is DiplomaticRelation.Ally;
        public bool IsNeutral(Allegiance other) => RelationshipWith(other) is DiplomaticRelation.Neutral;

        [Pure]
        DiplomaticRelation RelationshipWith([NotNull] Allegiance other)
        {
            if(other.Motherland.Equals(Nation.Stateless) || Motherland.Equals(Nation.Stateless))
                return DiplomaticRelation.Neutral;

            return other.Motherland.Equals(Motherland)
                ? DiplomaticRelation.Ally
                : DiplomaticRelation.Enemy;
        }
    }
}