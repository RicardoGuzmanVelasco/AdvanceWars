using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain.Map;
using UnityEngine;

namespace AdvanceWars.Runtime.Application
{
    public class DrawMap
    {
        readonly Map map;
        readonly MapView mapView;
        readonly CursorView cursorView;

        public DrawMap(Map map, MapView mapView, CursorView cursorView)
        {
            this.map = map;
            this.mapView = mapView;
            this.cursorView = cursorView;
        }

        public async Task Run()
        {
            cursorView.MoveTo(Vector2Int.zero);
            await mapView.Draw(map);
        }
    }
}