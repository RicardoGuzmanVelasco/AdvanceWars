using System.Threading.Tasks;

namespace AdvanceWars.Runtime.Application
{
    public interface DayView
    {
        public Task StartDay(int day);
    }
}