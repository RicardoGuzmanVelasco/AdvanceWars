using System.Collections;
using System.Collections.Generic;
using AdvanceWars.Runtime.Domain.Troops;
using UnityEngine;

namespace AdvanceWars.Runtime.Domain.Orders
{
    public record Situation(Map.Map Map, Treasury Treasury)
    {
        public int WarFunds => Treasury.WarFunds;

        public Map.Map.Space WhereIs(Battalion battalion) => Map.WhereIs(battalion);

        public Map.Map.Space WhereIs(Spawner spawner) => Map.WhereIs(spawner);

        public IEnumerable<Battalion> EnemyBattalionsInRangeOfFire(Battalion battalion)
        {
            return Map.EnemyBattalionsInRangeOfFire(battalion);
        }

        public IEnumerable<Vector2Int> RangeOfMovement(Battalion battalion)
        {
            return Map.RangeOfMovement(battalion);
        }

        public bool CanAfford(Unit unit)
        {
            return Treasury.CanAfford(unit);
        }

        public void ManageLogistics(Allegiance allegiance)
        {
            foreach (var space in FriendlyTerrainSpaces(allegiance))
            {
                space.ReportIncome(Treasury);
                
                if (space.FriendlyOccupant)
                {
                    space.HealOccupant();
                    space.ReplenishOccupantAmmo();
                }
            }
        }

        private IEnumerable<Map.Map.Space> FriendlyTerrainSpaces(Allegiance allegiance)
        {
            return Map.FriendlyTerrainSpaces(allegiance);
        }

    }
}