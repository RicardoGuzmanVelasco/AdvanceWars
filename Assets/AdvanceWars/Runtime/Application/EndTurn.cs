using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;

namespace AdvanceWars.Runtime.Application
{
    public class EndTurn
    {
        readonly Game game;
        readonly DayView dayView;

        public EndTurn(Game game, DayView dayView)
        {
            this.game = game;
            this.dayView = dayView;
        }

        public Task Run()
        {
            game.EndTurn();
            return dayView.StartDay(game.Day);
        }
    }
}