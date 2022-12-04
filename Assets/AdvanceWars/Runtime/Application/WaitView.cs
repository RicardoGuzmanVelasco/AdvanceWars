using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Runtime.Application
{
    public interface WaitView
    {
        public Task Wait(Battalion battalion);
    }
}