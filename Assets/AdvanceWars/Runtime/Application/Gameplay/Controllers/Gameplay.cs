using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;

namespace AdvanceWars.Runtime.Application
{
    public class Gameplay
    {
        private readonly Game game;
        private readonly PlayTurn playTurn;
        private readonly DrawMap drawMap;

        public Gameplay(Game game, PlayTurn playTurn, DrawMap drawMap)
        {
            this.game = game;
            this.playTurn = playTurn;
            this.drawMap = drawMap;
        }

        public async Task Run()
        {
            await drawMap.Run();
            while (game.Playing)
            {
                await playTurn.Run();
                await Task.Yield();
            }
        }
    }
}