using System.Threading.Tasks;

namespace AdvanceWars.Runtime.Application
{
    public class Gameplay
    {
        private readonly PlayTurn playTurn;
        private readonly DrawMap drawMap;

        public Gameplay(PlayTurn playTurn, DrawMap drawMap)
        {
            this.playTurn = playTurn;
            this.drawMap = drawMap;
        }

        public async Task Run()
        {
            await drawMap.Run();
            while (true)
            {
                await playTurn.Run();
                await Task.Yield();
            }
        }
    }
}