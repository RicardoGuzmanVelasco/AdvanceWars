using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Presentation;

namespace AdvanceWars.Runtime.Application
{
    public class EndTurn
    {
        readonly Game game;
        readonly DayView dayView;
        readonly TurnView turnView;
        readonly SelectSpace selectSpace;
        readonly EndTurnInput endTurnInput;

        public EndTurn(Game game, DayView dayView, TurnView turnView, SelectSpace selectSpace, EndTurnInput endTurnInput)
        {
            this.game = game;
            this.dayView = dayView;
            this.turnView = turnView;
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
            var currentDay = game.Day;
            game.EndTurn();

            await selectSpace.Deselect();
            if (currentDay != game.Day)
                await dayView.StartDay(game.Day);
            
            await turnView.SetTurn(game.NationInTurn);
        }
    }
}