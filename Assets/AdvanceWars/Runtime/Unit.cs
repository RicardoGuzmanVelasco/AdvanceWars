using AdvanceWars.Runtime.Domain.Troops;
using UnityEngine;

namespace AdvanceWars.Runtime.Data
{
    [CreateAssetMenu(fileName = "Unit", menuName = "AW/Unit")]
    public class Unit : ScriptableObject
    {
        [field: SerializeField] public Color Color { get; private set; }

        public Battalion CreateBattalion()
        {
            return new Battalion
            {
                Motherland = default,
                Unit = new Domain.Troops.Unit() { Id = name },
                Forces = 100,
                AmmoRounds = 0
            };
        }
    }
}