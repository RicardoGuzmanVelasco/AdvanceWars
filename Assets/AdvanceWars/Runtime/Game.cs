using System;
using System.Collections.Generic;

namespace AdvanceWars.Runtime
{
    public class Game
    {
        readonly Operation operation;

        public event Action<NewTurnOfDayArgs> NewTurnOfDay
        {
            add => operation.NewTurnOfDay += value;
            remove => operation.NewTurnOfDay -= value;
        }


        public Game(IEnumerable<CommandingOfficer> officers)
        {
            operation = new Operation(officers);
        }

        public void NextTurn()
        {
            operation.NextTurn();
        }
    }
}