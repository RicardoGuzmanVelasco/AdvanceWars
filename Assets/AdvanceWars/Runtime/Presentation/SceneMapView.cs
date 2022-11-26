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
        [SerializeField] SpaceView prefab;

        public async Task Draw(IEnumerable<KeyValuePair<Vector2Int, Map.Space>> map)
        {
            foreach(var space in map)
            {
                var instance = Instantiate(prefab, (Vector2)space.Key, Quaternion.identity);
                var terrain = Resources.LoadAll<Runtime.Data.Terrain>("").Single(x => x.name == space.Value.Terrain.Id);
                instance.GetComponent<SpriteRenderer>().color = terrain.Color;
            }
        }
    }
}