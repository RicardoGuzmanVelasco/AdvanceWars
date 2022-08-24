using System;
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

        public event Action<NewTurnOfDayArgs> NewTurnOfDay
        {
            add => operation.NewTurnOfDay += value;
            remove => operation.NewTurnOfDay -= value;
        }

        public Game(IEnumerable<CommandingOfficer> officers, [NotNull] IDictionary<Nation, Player> players)
        {
            Require(players.Values.All(p => p != null)).True();

            this.players = players;
            cursor = new Cursor();
            operation = new Operation(officers);
        }

        public Player ActivePlayer => players[operation.ActiveCommandingOfficer.Motherland];
        public bool CursorIsEnabled => cursor.Enabled;

        public void EndCurrentTurn()
        {
            operation.EndTurn();
        }

        public void BeginNextTurn()
        {
            cursor.Enabled = true;
            operation.BeginTurn();
        }

        public void EndTurn()
        {
            operation.EndTurn();
        }
    }
}