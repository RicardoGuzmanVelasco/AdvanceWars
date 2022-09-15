using System;
using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public partial class Building : Terrain
    {
        readonly int maxSiegePoints;

        public Building(int siegePoints, Nation owner) : this(siegePoints)
        {
            Motherland = owner;
        }

        public Building(int siegePoints)
        {
            maxSiegePoints = SiegePoints = siegePoints;
        }

        public override bool IsUnderSiege
        {
            get
            {
                Require(this.Equals(Building.Unbesiegable)).False();
                
                return maxSiegePoints > SiegePoints;
            }
        }

        public override Building SiegeOutcome(Battalion besieger)
        {
            Require(IsBesiegable(besieger)).True();

            var resultPoints = Math.Max(0, SiegePoints - besieger.Platoons);

            return resultPoints == 0
                ? new Building(maxSiegePoints, besieger.Motherland)
                : new Building(resultPoints, Motherland);
        }

        public override void LiftSiege()
        {
            Require(SiegePoints).LesserThan(maxSiegePoints);
            SiegePoints = maxSiegePoints;
        }

        public override bool IsBesiegable(Battalion besieger)
        {
            return !IsAlly(besieger);;
        }
    }
}