using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;

namespace AdvanceWars.Runtime.Application
{
    public class PlayTurn
    {
        private readonly Game game;
        private readonly EndTurn endTurn;

        public PlayTurn(Game game, EndTurn endTurn)
        {
            this.game = game;
            this.endTurn = endTurn;
        }

        public async Task Run()
        {
            await endTurn.Listen();
        }
    }
}