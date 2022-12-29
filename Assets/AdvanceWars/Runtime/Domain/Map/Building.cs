using System;
using AdvanceWars.Runtime.Domain.Troops;
using UnityEngine;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Map
{
    public partial class Building : Terrain
    {
        const int ReinforcesPerTurn = 20;

        readonly Military owner;

        protected override int Income { get; }

        public Building(int maxSiegePoints, Nation motherland) : base(maxSiegePoints)
        {
            Motherland = motherland;
        }

        public Building(int maxSiegePoints, Nation motherland, int income, Military owner) 
            : this(maxSiegePoints, motherland)
        {
            Income = income;
            this.owner = owner;
        }

        public Building(int maxSiegePoints) : base(maxSiegePoints)
        {
        }

        public override bool IsUnderSiege
        {
            get
            {
                Require(Equals(Unbesiegable)).False();

                return SiegePoints < SiegePoints.Ceil;
            }
        }

        public override Building SiegeOutcome(Battalion besieger)
        {
            Require(IsBesiegable(besieger)).True();

            var resultPoints = Math.Max(0, SiegePoints - besieger.Platoons);

            return resultPoints == 0
                ? new Building(SiegePoints.Ceil, besieger.Motherland)
                : new Building(resultPoints, Motherland);
        }

        public override void LiftSiege()
        {
            Require(SiegePoints.Value).LesserThan(SiegePoints.Ceil);
            SiegePoints = SiegePoints.Ceil;
        }

        public override bool IsBesiegable(Battalion besieger)
        {
            return !IsAlly(besieger);
        }

        public override bool CanHeal(Battalion patient, Treasury treasury)
        {
            return patient.ServiceBranch.Equals(owner) 
                && ReinforcesAmount(patient, treasury) > 0 
                && patient.IsAlly(this);
        }

        public override void Heal(Battalion patient, Treasury treasury)
        {
            Require(CanHeal(patient, treasury)).True();

            var reinforcesAmount = ReinforcesAmount(patient, treasury);
            patient.Heal(reinforcesAmount);

            var repairPrice = reinforcesAmount * patient.PricePerSoldier;
            if(repairPrice > 0)
                treasury.Spend(repairPrice);
        }

        int ReinforcesAmount(Battalion patient, Treasury treasury)
        {
            var idealReinforcesAmount = Mathf.Clamp(
                Battalion.MaxForces - patient.Forces,
                0,
                ReinforcesPerTurn);
            var reinforcesCost = patient.PricePerSoldier * idealReinforcesAmount;
            var repairBudget = reinforcesCost < treasury.WarFunds ? reinforcesCost : treasury.WarFunds;

            return patient.PricePerSoldier > 0
                ? repairBudget / patient.PricePerSoldier
                : idealReinforcesAmount;
        }
    }
}