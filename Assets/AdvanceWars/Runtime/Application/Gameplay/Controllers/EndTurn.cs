using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Inputs;

namespace AdvanceWars.Runtime.Application
{
    public class EndTurn
    {
        readonly Game game;
        readonly SelectSpace selectSpace;
        readonly EndTurnInput endTurnInput;

        public EndTurn(Game game, SelectSpace selectSpace, EndTurnInput endTurnInput)
        {
            this.game = game;
            this.selectSpace = selectSpace;
            this.endTurnInput = endTurnInput;
        }

        public async Task Listen()
        {
            await endTurnInput.Listen();
            await Run();
        }
        
        private async Task Run()
        {
            game.EndTurn();

            await selectSpace.Deselect();
        }
    }
}