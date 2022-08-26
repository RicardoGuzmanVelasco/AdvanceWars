using System;
using JetBrains.Annotations;

namespace AdvanceWars.Runtime
{
    public class Building : Terrain
    {
        public int SiegePoints { get; set; }
        int MaxSiegePoints { get; }

        public Building(int siegePoints)
        {
            MaxSiegePoints = SiegePoints = siegePoints;
        }

        public Building(int siegePoints, Nation owner) : this(siegePoints)
        {
            Motherland = owner;
        }

        [Pure]
        public Building SiegeOutcome([NotNull] Battalion besieger)
        {
            var resultPoints = Math.Max(0, SiegePoints - besieger.Platoons);

            return resultPoints == 0
                ? new Building(MaxSiegePoints, besieger.Motherland)
                : new Building(resultPoints, Motherland);
        }

        public void LiftSiege()
        {
            SiegePoints = MaxSiegePoints;
        }
    }
}