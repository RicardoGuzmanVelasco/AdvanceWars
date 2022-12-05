using System.Threading.Tasks;

namespace AdvanceWars.Runtime.Application
{
    public interface PlayersConfigurationView
    {
        public Task SetPlayers(int playerAmount);
    }
}