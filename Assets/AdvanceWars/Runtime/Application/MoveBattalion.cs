using System.Collections.Generic;
using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Orders.Maneuvers;
using AdvanceWars.Runtime.Domain.Troops;
using UnityEngine;

namespace AdvanceWars.Runtime.Application
{
    public class MoveBattalion
    {
        readonly Situation situation;
        readonly MovementView movementView;

        public MoveBattalion(Situation situation, MovementView movementView)
        {
            this.situation = situation;
            this.movementView = movementView;
        }
        
        public Task Execute(Battalion battalion, Vector2Int targetPos)
        {
            if (situation.SpaceAt(targetPos).Occupant == battalion) return Task.CompletedTask;

            var movementManeuver= new MovementManeuver(battalion, new List<Map.Space>() {situation.SpaceAt(targetPos) });
            movementManeuver.Apply(situation);
            return movementView.Move(battalion, targetPos);
        }
    }
}