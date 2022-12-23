using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Presentation;
using ModestTree;

namespace AdvanceWars.Runtime.Application
{
    public class Gameplay
    {
        private readonly PlayTurn playTurn;

        public Gameplay(PlayTurn playTurn)
        {
            this.playTurn = playTurn;
        }

        public async Task Run()
        {
            while (true)
            {
                await playTurn.Run();
                await Task.Yield();
            }
        }
    }
}