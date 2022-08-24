using System;
using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime.DataStructures;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Operation
    {
        public event Action<Nation, int> NewTurnOfDay;

        readonly RotarySwitch<CommandingOfficer> officers;

        public Operation(IEnumerable<CommandingOfficer> commandingOfficers)
        {
            Require(commandingOfficers.Any()).True();
            officers = new RotarySwitch<CommandingOfficer>(commandingOfficers);
            NewTurnOfDay += (_, _) => { };
        }

        public int Day => officers.Round;
        public CommandingOfficer ActiveCommandingOfficer => officers.Current;

        public void NextTurn()
        {
            officers.Next();
            ActiveCommandingOfficer.BeginTurn();
            NewTurnOfDay!.Invoke(ActiveCommandingOfficer.Motherland, Day);
        }
    }
}