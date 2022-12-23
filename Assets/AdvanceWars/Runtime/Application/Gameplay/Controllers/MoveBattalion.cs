using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Orders.Maneuvers;
using UnityEngine;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Application
{
    public class MoveBattalion
    {
        readonly MovementView movementView;
        readonly Game game;
        readonly Map map;

        public MoveBattalion(Game game, Map map, MovementView movementView)
        {
            this.movementView = movementView;
            this.game = game;
            this.map = map;
        }

        public Task Execute(Vector2Int targetPos)
        {
            var selectedBattalion = game.SelectedBattalion;
            var originSpace = map.WhereIs(selectedBattalion)!;

            if (!game.CurrentCommandingOfficer.AvailableTacticsAt(originSpace).Contains(Tactic.Move))
                return Task.CompletedTask;
            
            var movementManeuver = Maneuver.Move
            (
                selectedBattalion,
                new List<Map.Space> { map.SpaceAt(targetPos) }
            );
            
            game.CurrentCommandingOfficer.Order(movementManeuver);

            return movementView.Move(selectedBattalion, targetPos);
        }
    }
}