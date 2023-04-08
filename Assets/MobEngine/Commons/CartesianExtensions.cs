using System.Collections.Generic;
using UnityEngine;

namespace AdvanceWars.Runtime
{
    public static class CartesianExtensions
    {
        public static IEnumerable<Vector2Int> AdjacentsCoords(this Vector2Int from)
        {
            return new[]
            {
                from + Vector2Int.up,
                from + Vector2Int.down,
                from + Vector2Int.left,
                from + Vector2Int.right
            };
        }

        public static bool IsDirection(this Vector2Int target)
        {
            return target == Vector2Int.up ||
                   target == Vector2Int.down ||
                   target == Vector2Int.left ||
                   target == Vector2Int.right;
        }
    }
}