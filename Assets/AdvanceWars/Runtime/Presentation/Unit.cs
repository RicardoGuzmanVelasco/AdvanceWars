using AdvanceWars.Runtime.Domain.Troops;
using UnityEngine;

namespace AdvanceWars.Runtime.Data
{
    [CreateAssetMenu(fileName = "Unit", menuName = "AW/Unit")]
    public class Unit : ScriptableObject
    {
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public int MovementRate { get; private set; } = 1;
        [field: SerializeField] public Propulsion Propulsion { get; private set; }
        
        public Battalion CreateBattalion(Nation motherland)
        {
            return new Battalion
            {
                Motherland = motherland,
                Unit = new Domain.Troops.Unit()
                {
                    Id = name,
                    Mobility = MovementRate,
                    Propulsion = this.Propulsion
                },
                Forces = 100,
                AmmoRounds = 0
            };
        }
    }
}