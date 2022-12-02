using System.Collections.Generic;
using AdvanceWars.Runtime.Domain.Troops;
using UnityEngine;

namespace AdvanceWars.Runtime.Data
{
    [CreateAssetMenu(fileName = "Terrain", menuName = "AW/Terrain")]
    public class Terrain : ScriptableObject
    {
        [field: SerializeField] public Color Color { get; private set; }

        public static implicit operator Domain.Map.Terrain(Terrain terrain)
        {
            return new Domain.Map.Terrain(new Dictionary<Propulsion, int>(), new List<Propulsion>())
                { Id = terrain.name };
        }
    }
}