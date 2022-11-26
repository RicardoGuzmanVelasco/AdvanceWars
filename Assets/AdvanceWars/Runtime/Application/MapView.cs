using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AdvanceWars.Runtime.Application
{
    public interface MapView
    {
        Task Draw(IDictionary<Vector2Int, Space> map);
    }
}