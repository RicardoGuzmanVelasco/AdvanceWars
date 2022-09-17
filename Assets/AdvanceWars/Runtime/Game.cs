using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Game
    {
        readonly IDictionary<Nation, Player> players;
        readonly Operation operation;
        readonly Cursor cursor;

        public event Action<Vector2Int> CursorMoved = _ => { };

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

        public Game
        (
            [NotNull] IEnumerable<CommandingOfficer> officers,
            [NotNull] IDictionary<Nation, Player> players,
            Map battleground = null
        )
        {
            Require(players.Values.All(p => p != null)).True();

            this.players = players;
            operation = new Operation(officers, battleground ?? Map.Null);
            cursor = new Cursor { WhereIs = Vector2Int.zero };
        }

        public Player ActivePlayer => players[operation.NationInTurn];
        public Vector2Int CursorCoord => cursor.WhereIs;


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

        public void PutCursorAt(Vector2Int targetCoord)
        {
            Require(targetCoord != CursorCoord).True();
            Require(operation.Battleground.IsInsideBounds(targetCoord)).True();

            cursor.WhereIs = targetCoord;
        }
    }
}