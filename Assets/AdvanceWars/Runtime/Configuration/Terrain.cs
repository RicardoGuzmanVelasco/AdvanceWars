using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime.Domain.Troops;
using UnityEngine;

namespace AdvanceWars.Runtime.Data
{
    [CreateAssetMenu(fileName = "Terrain", menuName = "AW/Terrain")]
    public class Terrain : ScriptableObject
    {
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public PropulsionCost[] PropulsionCosts { get; private set; }
        [field: SerializeField] public Propulsion[] BlockedPropulsions { get; private set; }

        public static implicit operator Domain.Map.Terrain(Terrain terrain)
        {
            var propulsionCosts = new Dictionary<Domain.Troops.Propulsion, int>();
            foreach (var cost in terrain.PropulsionCosts)
            {
                propulsionCosts.Add(cost.Propulsion, cost.MoveCost);
            }
            return new Domain.Map.Terrain(propulsionCosts, 
                    terrain.BlockedPropulsions.Select(x => (Domain.Troops.Propulsion) x))
                { Id = terrain.name };
        }
    }
}