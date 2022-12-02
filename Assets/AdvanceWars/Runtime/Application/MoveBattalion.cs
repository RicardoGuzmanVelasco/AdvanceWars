using System;
using System.Collections.Generic;
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
        readonly Situation situation;
        readonly MovementView movementView;
        readonly SelectionView selectView;
        readonly SelectBattalion selectBattalion;
        readonly Game game;

        public MoveBattalion(Situation situation, MovementView movementView, SelectionView selectView, Game game)
        {
            this.situation = situation;
            this.movementView = movementView;
            this.selectView = selectView;
            this.game = game;
        }

        public Task Execute(Vector2Int targetPos)
        {
            Require(game.AnythingSelected).True();

            var selectedBattalion = game.SelectedBattalion;
            if(situation.SpaceAt(targetPos).Occupant == selectedBattalion)
                throw new NotImplementedException("Sacar el menú para esperar");

            var movementManeuver = new MovementManeuver
            (
                selectedBattalion,
                new List<Map.Space> { situation.SpaceAt(targetPos) }
            );

            movementManeuver.Apply(situation);
            game.Deselect();

            return Task.WhenAll
            (
                movementView.Move(selectedBattalion, targetPos),
                selectView.Hide()
            );
        }
    }
}