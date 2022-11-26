using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvanceWars.Runtime.Application;
using AdvanceWars.Runtime.Domain.Map;
using UnityEngine;

namespace AdvanceWars.Runtime.Presentation
{
    public class SceneMapView : MonoBehaviour, MapView
    {
        [SerializeField] SpaceView spacePrefab;
        [SerializeField] BattalionView battalionPrefab;

        public async Task Draw(IEnumerable<KeyValuePair<Vector2Int, Map.Space>> map)
        {
            DrawTerrains(map);
            DrawBattalions(map);
        }

        void DrawBattalions(IEnumerable<KeyValuePair<Vector2Int, Map.Space>> map)
        {
            foreach(var kvpSpace in map)
            {
                if(!kvpSpace.Value.IsOccupied)
                    continue;

                var instance = Instantiate(battalionPrefab, (Vector2)kvpSpace.Key, Quaternion.identity);
                var loadAll = Resources.LoadAll<Data.Unit>("");
                var unit = loadAll.Single(x => x.name == kvpSpace.Value.Occupant.UnitId);
                instance.GetComponent<SpriteRenderer>().color = unit.Color;
            }
        }

        void DrawTerrains(IEnumerable<KeyValuePair<Vector2Int, Map.Space>> map)
        {
            foreach(var space in map)
            {
                var instance = Instantiate(spacePrefab, (Vector2)space.Key, Quaternion.identity);
                var terrain = Resources.LoadAll<Data.Terrain>("").Single(x => x.name == space.Value.Terrain.Id);
                instance.GetComponent<SpriteRenderer>().color = terrain.Color;
            }
        }
    }
}