using UnityEngine;

namespace AdvanceWars.Runtime.Data
{
    [CreateAssetMenu(fileName = "Propulsion", menuName = "AW/Propulsion")]
    public class Propulsion : ScriptableObject
    {
        public static implicit operator Domain.Troops.Propulsion(Propulsion propulsion)
        {
            return new Domain.Troops.Propulsion(propulsion.name);
        }
    }
}