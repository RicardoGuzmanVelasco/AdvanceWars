using System.Collections.Generic;
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
            public Battalion Guest { get; private set; } = Battalion.Null;

            public bool IsOccupied => Occupant is not INull;
            bool HasGuest => Guest is not INull;

            public virtual bool IsBesiegable => Terrain.IsBesiegable(besieger: Occupant);
            public IEnumerable<Unit> SpawnableUnits => Terrain.SpawnableUnits;

            public bool CanEnter(Battalion battalion) => !IsOccupied || CanBeInvited(battalion);

            bool CanBeInvited(Battalion other) => IsOccupied && other.CanMergeInto(Occupant);

            public bool IsCrossableBy(Battalion battalion) => !IsHostileTo(battalion);

            public int MoveCostOf(Battalion battalion) => Terrain.MoveCostOf(battalion.Propulsion);

            public void Besiege()
            {
                Require(IsOccupied).True();
                Require(IsBesiegable).True();

                var outcome = Terrain.SiegeOutcome(Occupant);
                Terrain.SiegePoints = outcome.SiegePoints;
            }

            public bool IsHostileTo(Allegiance other)
            {
                return IsOccupied && Occupant.IsEnemy(other);
            }

            public void Enter(Battalion battalion)
            {
                if(IsOccupied)
                {
                    StopBy(battalion);
                }
                else
                {
                    Occupy(battalion);
                }
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

            public void StopBy(Battalion battalion)
            {
                Require(CanBeInvited(battalion)).True();
                Guest = battalion;
            }

            public void ExpelGuest()
            {
                Require(HasGuest).True();
                Guest = Battalion.Null;
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
                Occupant.AmmoRounds += 2;
            }

            public void HealOccupant()
            {
                Terrain.Heal(Occupant);
            }

            public void SpawnBattalionHere(Unit ofUnit)
            {
                Occupy(Terrain.SpawnBattalion(ofUnit));
            }
        }
    }
}