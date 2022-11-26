using System.Collections.Generic;
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
                Instantiate(prefab, (Vector2)space.Key, Quaternion.identity);
            }
        }
    }
}