using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;

namespace AdvanceWars.Runtime.Application
{
    public class EndTurn
    {
        readonly Game game;
        readonly DayView dayView;
        readonly TurnView turnView;

        public EndTurn(Game game, DayView dayView, TurnView turnView)
        {
            this.game = game;
            this.dayView = dayView;
            this.turnView = turnView;
        }

        public async Task Run()
        {
            var currentDay = game.Day;
            game.EndTurn();

            if (currentDay != game.Day)
                await dayView.StartDay(game.Day);
            
            await turnView.SetTurn(game.NationInTurn);
        }
    }
}