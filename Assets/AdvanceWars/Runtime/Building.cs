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
                Require(this.Equals(new UnbesiegableSpecialCase())).False();
                
                return maxSiegePoints > SiegePoints;
            }
        }

        public override Building SiegeOutcome(Battalion besieger)
        {
            Require(besieger.IsAlly(this)).False();

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
    }
}