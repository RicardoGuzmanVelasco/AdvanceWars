using System;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public class Building : Terrain
    {
        public int SiegePoints { get; set; }
        int MaxSiegePoints { get; }

        Nation owner;

        public Building(int siegePoints)
        {
            MaxSiegePoints = SiegePoints = siegePoints;
        }

        public Building(int siegePoints, Nation owner) : this(siegePoints)
        {
            this.owner = owner;
        }

        [Pure]
        public DiplomaticRelation RelationshipWith([NotNull] Battalion other)
        {
            if(other.Motherland.Equals(owner))
                return DiplomaticRelation.Ally;
            if(other.Motherland.IsStateless || owner.IsStateless)
                return DiplomaticRelation.Neutral;
            else
                return DiplomaticRelation.Enemy;
        }

        [Pure]
        public Building SiegeOutcome([NotNull] Battalion besieger)
        {
            var resultPoints = Math.Max(0, SiegePoints - besieger.Platoons);

            return resultPoints == 0
                ? new Building(MaxSiegePoints, besieger.Motherland)
                : new Building(resultPoints, owner);
        }

        public void LiftSiege()
        {
            SiegePoints = MaxSiegePoints;
        }
    }
}