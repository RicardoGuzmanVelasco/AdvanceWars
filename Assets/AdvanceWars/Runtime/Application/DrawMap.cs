using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain.Map;

namespace AdvanceWars.Runtime.Application
{
    public class DrawMap
    {
        readonly MapView view;
        readonly Map map;

        public DrawMap(MapView view, Map map)
        {
            this.view = view;
            this.map = map;
        }

        public async Task Run()
        {
            await view.Draw(map);
        }
    }
}