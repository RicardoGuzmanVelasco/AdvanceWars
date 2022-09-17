﻿using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Game
    {
        readonly IDictionary<Nation, Player> players;
        readonly Operation operation;
        readonly Cursor cursor;

        public event Action<bool> CursorEnableChanged
        {
            add => cursor.EnableChanged += value;
            remove => cursor.EnableChanged -= value;
        }

        public event Action<NewTurnOfDayArgs> NewTurnOfDay
        {
            add => operation.NewTurnOfDay += value;
            remove => operation.NewTurnOfDay -= value;
        }

        public Game(IEnumerable<CommandingOfficer> officers, [NotNull] IDictionary<Nation, Player> players)
        {
            Require(players.Values.All(p => p != null)).True();

            this.players = players;
            operation = new Operation(officers);

            cursor = new Cursor();
        }

        public Player ActivePlayer => players[operation.NationInTurn];

        public void Begin()
        {
            cursor.Enable();
        }

        public void EndCurrentTurn()
        {
            cursor.Disable();
            operation.EndTurn();
        }

        public void BeginNextTurn()
        {
            cursor.Enable();
            operation.BeginTurn();
        }
    }
}