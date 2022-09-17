using System;
using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime.DataStructures;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Operation
    {
        readonly RotarySwitch<CommandingOfficer> officers;
        public Map Battleground { get; } = Map.Null;
        public event Action<NewTurnOfDayArgs> NewTurnOfDay = _ => { };


        public Operation([NotNull] IEnumerable<CommandingOfficer> commandingOfficers, Map battleground = null)
        {
            Require(commandingOfficers.Any()).True();
            officers = new RotarySwitch<CommandingOfficer>(commandingOfficers);
            this.Battleground = battleground;
        }

        public int Day => officers.Round;
        public Nation NationInTurn => officers.Current.Motherland;

        public void BeginTurn() { }

        public void EndTurn()
        {
            officers.Next();
            officers.Current.BeginTurn();
            NewTurnOfDay.Invoke(new NewTurnOfDayArgs(NationInTurn, Day));
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