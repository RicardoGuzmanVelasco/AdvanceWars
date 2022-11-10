using System;
using AdvanceWars.Runtime.Domain.Troops;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Map
{
    public partial class Building : Terrain
    {
        private const int ReinforcesPerTurn = 20;

        readonly int maxSiegePoints;
        readonly int income;
        
        public override int Income => income;

        public Building(int maxSiegePoints, Nation owner) : this(maxSiegePoints)
        {
            Motherland = owner;
        }
        public Building(int maxSiegePoints, Nation owner, int income) : this(maxSiegePoints, owner)
        {
            this.income = income;
        }
        

        public Building(int maxSiegePoints)
        {
            this.maxSiegePoints = SiegePoints = maxSiegePoints;
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
            return !IsAlly(besieger);
        }

        public override void Heal(Battalion occupant)
        {
            base.Heal(occupant);
            occupant.Heal(ReinforcesPerTurn);
        }
    }
}