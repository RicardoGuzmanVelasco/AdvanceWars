using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain.Troops;
using UnityEngine;

namespace AdvanceWars.Runtime.Application
{
    public interface MovementView
    {
        public Task Move(Battalion battalion, Vector2Int targetPos);
    }
}