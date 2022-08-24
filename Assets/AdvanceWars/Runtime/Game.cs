using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Game
    {
        public event Action<bool> CursorEnableChanged = _ => { };

        readonly IDictionary<Nation, Player> players;
        readonly Operation operation;
        readonly Cursor cursor;

        public Player ActivePlayer => players[operation.ActiveCommandingOfficer.Motherland];

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

        public void Begin()
        {
            EnableCursor();
        }

        void EnableCursor()
        {
            if(cursor.Enabled)
                return;

            cursor.Enabled = true;
            CursorEnableChanged.Invoke(cursor.Enabled);
        }

        public void EndCurrentTurn()
        {
            DisableCursor();

            operation.EndTurn();
        }

        void DisableCursor()
        {
            if(!cursor.Enabled)
                return;

            cursor.Enabled = false;
            CursorEnableChanged.Invoke(cursor.Enabled);
        }

        public void BeginNextTurn()
        {
            EnableCursor();

            operation.BeginTurn();
        }
    }
}