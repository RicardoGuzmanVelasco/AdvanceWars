using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;

namespace AdvanceWars.Runtime.Application
{
    public class PlayTurn
    {
        readonly Game game;
        readonly EndTurn endTurn;
        readonly TurnView turnView;
        
        public PlayTurn(Game game, EndTurn endTurn, TurnView turnView)
        {
            this.game = game;
            this.endTurn = endTurn;
            this.turnView = turnView;
        }

        public async Task Run()
        {
            await turnView.SetTurn(game.NationInTurn);

            await endTurn.Listen();
        }
    }
}