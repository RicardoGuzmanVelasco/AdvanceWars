using System;
using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime.DataStructures;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Operation
    {
        public event Action<NewTurnOfDayArgs> NewTurnOfDay = _ => { };

        readonly RotarySwitch<CommandingOfficer> officers;

        public Operation(IEnumerable<CommandingOfficer> commandingOfficers)
        {
            Require(commandingOfficers.Any()).True();
            officers = new RotarySwitch<CommandingOfficer>(commandingOfficers);
        }

        public int Day => officers.Round;
        public CommandingOfficer ActiveCommandingOfficer => officers.Current;

        public void BeginTurn() { }

        public void EndTurn()
        {
            officers.Next();
            ActiveCommandingOfficer.BeginTurn();
            NewTurnOfDay!.Invoke(new NewTurnOfDayArgs(ActiveCommandingOfficer.Motherland, Day));
        }
    }

    public struct NewTurnOfDayArgs
    {
        public Nation Nation { get; }
        public int Day { get; }

        public NewTurnOfDayArgs(Nation nation, int day)
        {
            Nation = nation;
            Day = day;
        }
    }
}