using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Game
    {
        public event Action<bool> CursorEnableChanged
        {
            add => cursor.CursorEnableChanged += value;
            remove => cursor.CursorEnableChanged -= value;
        }

        public event Action<NewTurnOfDayArgs> NewTurnOfDay
        {
            add => operation.NewTurnOfDay += value;
            remove => operation.NewTurnOfDay -= value;
        }

        readonly IDictionary<Nation, Player> players;
        readonly Operation operation;
        readonly Cursor cursor;

        public Player ActivePlayer => players[operation.ActiveCommandingOfficer.Motherland];

        public Game(IEnumerable<CommandingOfficer> officers, [NotNull] IDictionary<Nation, Player> players)
        {
            Require(players.Values.All(p => p != null)).True();

            this.players = players;
            operation = new Operation(officers);

            cursor = new Cursor();
        }

        public void Begin()
        {
            cursor.EnableCursor();
        }

        public void EndCurrentTurn()
        {
            cursor.DisableCursor();

            operation.EndTurn();
        }

        public void BeginNextTurn()
        {
            cursor.EnableCursor();

            operation.BeginTurn();
        }
    }
}