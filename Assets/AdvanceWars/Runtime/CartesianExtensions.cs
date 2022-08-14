using System.Collections.Generic;
using UnityEngine;

namespace AdvanceWars.Runtime
{
    public static class CartesianExtensions
    {
        public static IEnumerable<Vector2Int> CoordsAdjacentsOf(this Vector2Int from)
        {
            return new[]
            {
                from + Vector2Int.up,
                from + Vector2Int.down,
                from + Vector2Int.left,
                from + Vector2Int.right
            };
        }
    }
}