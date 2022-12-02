using System;
using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;
using UnityEngine;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain
{
    public class Game
    {
        readonly IDictionary<Nation, Player> players;
        readonly Operation operation;
        protected Cursor cursor;

        Vector2Int? selected;

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
            Map.Map battleground = null
        )
        {
            Require(players.Values.All(p => p != null)).True();

            this.players = players;
            operation = new Operation(officers, battleground ?? Map.Map.Null);
            cursor = new Cursor { WhereIs = Vector2Int.zero };
        }

        public Player ActivePlayer => players[operation.NationInTurn];
        public Vector2Int CursorCoord => cursor.WhereIs;
        public bool CursorIsEnabled => cursor.IsEnabled;
        public bool AnythingSelected => selected.HasValue;

        public Battalion SelectedBattalion
        {
            get
            {
                Require(selected).Not.Null();
                return operation.BattalionAt(selected!.Value);
            }
        }

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
            CursorMoved.Invoke(targetCoord);
        }

        public void MoveCursorTowards(Vector2Int direction)
        {
            Require(direction.IsDirection()).True();

            PutCursorAt(CursorCoord + direction);
        }

        public void SelectAtCursor() => selected = CursorCoord;

        public void Deselect() => selected = null;


        public bool CanMoveCursorTowards(Vector2Int direction) =>
            operation.Battleground.IsInsideBounds(CursorCoord + direction);
    }
}