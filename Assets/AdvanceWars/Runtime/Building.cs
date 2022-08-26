using System;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Building : Terrain
    {
        public Building(int siegePoints)
        {
            MaxSiegePoints = SiegePoints = siegePoints;
        }

        public Building(int siegePoints, Nation owner) : this(siegePoints)
        {
            Motherland = owner;
        }

        public override Building SiegeOutcome(Battalion besieger)
        {
            Require(besieger.IsAlly(this)).False();

            var resultPoints = Math.Max(0, SiegePoints - besieger.Platoons);

            return resultPoints == 0
                ? new Building(MaxSiegePoints, besieger.Motherland)
                : new Building(resultPoints, Motherland);
        }

        public override void LiftSiege()
        {
            SiegePoints = MaxSiegePoints;
        }

        #region Special Cases
        internal static Building Unbesiegable { get; } = new UnbesiegableSpecialCase();

        public class UnbesiegableSpecialCase : Building
        {
            public UnbesiegableSpecialCase() : base(int.MaxValue) { }
        }
        #endregion
    }
}