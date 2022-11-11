using System.Collections;
using System.Collections.Generic;
using AdvanceWars.Runtime.Domain.Troops;
using UnityEngine;

namespace AdvanceWars.Runtime.Domain.Orders
{
    public class Situation : Allegiance
    {
        public Map.Map Map { private get; init; }
        public Treasury Treasury { private get; init; }

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

        public void ManageLogistics()
        {
            foreach (var space in FriendlyTerrainSpaces(this))
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