using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;

namespace AdvanceWars.Runtime.Application
{
    public class PlayTurn
    {
        readonly Game game;
        readonly EndTurn endTurn;
        readonly TurnView turnView;
        readonly DayView dayView;

        public PlayTurn(Game game, EndTurn endTurn, TurnView turnView, DayView dayView)
        {
            this.game = game;
            this.endTurn = endTurn;
            this.turnView = turnView;
            this.dayView = dayView;
        }

        public async Task Run()
        {
            if (game.FirstTurnOfDay)
                await dayView.StartDay(game.Day);
            
            await turnView.SetTurn(game.NationInTurn);

            await endTurn.Listen();
        }
    }
}