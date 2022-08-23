using System.Collections.Generic;
using System.Linq;

namespace AdvanceWars.Runtime
{
    public class Operation
    {
        readonly CommandingOfficer[] commandingOfficers;
        int currentTurn;

        public Operation(IEnumerable<CommandingOfficer> commandingOfficers)
        {
            this.commandingOfficers = commandingOfficers.ToArray();
        }

        public CommandingOfficer ActiveCommandingOfficer => commandingOfficers[currentTurn];
        public int Day { get; set; } = 1;

        public void NextTurn()
        {
            currentTurn = (currentTurn + 1) % commandingOfficers.Length;

            if(currentTurn == 0)
                Day++;
        }
    }
}