using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Runtime.Application
{
    public interface TurnView
    {
        Task SetTurn(Nation nationInTurn);
    }
}