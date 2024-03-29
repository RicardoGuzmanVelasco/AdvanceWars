﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Orders.Maneuvers;
using RGV.DesignByContract.Runtime;
using UnityEngine;

namespace AdvanceWars.Runtime.Application
{
    public class WaitBattalion
    {
        readonly Game game;
        readonly Map map;
        readonly WaitView waitView;
        readonly SelectionView selectView;

        public WaitBattalion(Game game, Map map, WaitView waitView, SelectionView selectView)
        {
            this.game = game;
            this.map = map;
            this.waitView = waitView;
            this.selectView = selectView;
        }

        public Task Execute()
        {
            var selectedBattalion = game.SelectedBattalion;
            var originSpace = map.WhereIs(selectedBattalion);
            
            if (!game.CurrentCommandingOfficer.AvailableTacticsAt(originSpace).Contains(Tactic.Wait))
                return Task.CompletedTask;

            var waitManeuver = Maneuver.Wait(selectedBattalion);
            
            game.CurrentCommandingOfficer.Order(waitManeuver);

            return waitView.Wait(selectedBattalion);
        }
    }
}