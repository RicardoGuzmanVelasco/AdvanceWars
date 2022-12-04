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
        readonly SelectionView selectView;
        readonly SelectBattalion selectBattalion;
        readonly Game game;
        readonly Map map;

        public MoveBattalion(Game game, Map map, MovementView movementView, SelectionView selectView)
        {
            this.movementView = movementView;
            this.selectView = selectView;
            this.game = game;
            this.map = map;
        }

        public Task Execute(Vector2Int targetPos)
        {
            Require(game.AnythingSelected).True();

            var selectedBattalion = game.SelectedBattalion;
            var originSpace = map.WhereIs(selectedBattalion)!;
            if(map.SpaceAt(targetPos) == originSpace)
                throw new NotImplementedException("Sacar el menú para esperar");

            if (!game.CurrentCommandingOfficer.AvailableTacticsAt(originSpace).Contains(Tactic.Move))
                return Task.CompletedTask;
            
            var movementManeuver = new MovementManeuver
            (
                selectedBattalion,
                new List<Map.Space> { map.SpaceAt(targetPos) }
            );
            
            game.CurrentCommandingOfficer.Order(movementManeuver);
            game.Deselect();

            return Task.WhenAll
            (
                movementView.Move(selectedBattalion, targetPos),
                selectView.Hide()
            );
        }
    }
}