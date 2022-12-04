using System;
using UnityEngine;

namespace AdvanceWars.Runtime.Data
{
    [Serializable]
    public struct PropulsionCost
    {
        [field: SerializeField] public Propulsion Propulsion { get; private set; }
        [field: SerializeField] public int MoveCost { get; private set; }
    }
}