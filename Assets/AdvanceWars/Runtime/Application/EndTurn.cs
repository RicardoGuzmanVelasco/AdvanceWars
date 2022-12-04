using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;

namespace AdvanceWars.Runtime.Application
{
    public class EndTurn
    {
        readonly Game game;
        readonly DayView dayView;
        readonly TurnView turnView;
        readonly SelectSpace selectSpace;

        public EndTurn(Game game, DayView dayView, TurnView turnView, SelectSpace selectSpace)
        {
            this.game = game;
            this.dayView = dayView;
            this.turnView = turnView;
            this.selectSpace = selectSpace;
        }

        public async Task Run()
        {
            var currentDay = game.Day;
            game.EndTurn();

            await selectSpace.Deselect();
            if (currentDay != game.Day)
                await dayView.StartDay(game.Day);
            
            await turnView.SetTurn(game.NationInTurn);
        }
    }
}