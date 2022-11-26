using System.Collections.Generic;
using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain.Map;
using UnityEngine;

namespace AdvanceWars.Runtime.Application
{
    public interface MapView
    {
        Task Draw(IEnumerable<KeyValuePair<Vector2Int, Map.Space>> map);
    }
}