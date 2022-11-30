using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AdvanceWars.Runtime.Domain.Troops;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Map
{
    public partial record Map
    {
        public partial class Space
        {
            public Terrain Terrain { get; set; } = Terrain.Null;

            public Battalion Occupant { get; private set; } = Battalion.Null;

            public bool IsOccupied => Occupant is not INull;

            public bool FriendlyOccupant => IsOccupied && Terrain.IsAlly(Occupant);
            public virtual bool IsBesiegable => Terrain.IsBesiegable(besieger: Occupant);
            public IEnumerable<Unit> SpawnableUnits => Terrain.SpawnableUnits;

            public bool CanEnter(Battalion battalion) => !IsOccupied || CanBeInvited(battalion);

            bool CanBeInvited(Battalion other) => IsOccupied && other.CanMergeInto(Occupant);

            public bool IsCrossableBy(Battalion battalion) => !IsHostileTo(battalion);

            public int MoveCostOf(Battalion battalion) => Terrain.MoveCostOf(battalion.Propulsion);

            //Usado para saber si se puede ejecutar alguna maniobra
            public bool ExclusivePresenceOfAlliesTo(Allegiance other) => (IsOccupied && Occupant.IsAlly(other)) || (!IsOccupied && Terrain.IsAlly(other));

            public bool IsHostileTo(Allegiance other) => IsOccupied && Occupant.IsEnemy(other);
            
            public void Besiege()
            {
                Require(IsOccupied).True();
                Require(IsBesiegable).True();

                var outcome = Terrain.SiegeOutcome(Occupant);
                Terrain.SiegePoints = outcome.SiegePoints;
            }

            public void Occupy(Battalion occupant)
            {
                Require(IsOccupied).False();
                Occupant = occupant;
            }

            public void Unoccupy()
            {
                Require(IsOccupied).True();

                Occupant = Battalion.Null;

                if(Terrain.IsUnderSiege)
                    Terrain.LiftSiege();
            }

            public void ReportCasualties(int forcesAfter)
            {
                Require(IsOccupied).True();

                Occupant.Forces = forcesAfter;
                
                if(Occupant.Forces <= 0)
                    Unoccupy();
            }

            public void ConsumeAmmoRound()
            {
                Occupant.AmmoRounds--;
            }

            public void ReplenishOccupantAmmo()
            {
                Require(Occupant.IsAlly(Terrain)).True();
                Occupant.AmmoRounds += 2;
            }

            public void HealOccupant(Treasury treasury)
            {
                Require(CanHealOccupant(treasury)).True();
                Terrain.Heal(Occupant, treasury);
            }

            public void SpawnHere(Unit ofUnit)
            {
                Occupy(Terrain.SpawnBattalion(ofUnit));
            }

            public void ReportIncome([NotNull]Treasury treasury) => Terrain.ReportIncome(treasury);

            public bool CanHealOccupant(Treasury treasury)
            {
                return Terrain.CanHeal(Occupant, treasury);
            }
        }
    }
}