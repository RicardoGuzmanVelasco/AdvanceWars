using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace AdvanceWars.Runtime
{
    public record Map(int SizeX, int SizeY)
    {
        public IEnumerable<Vector2Int> RangeOfMovement(Vector2Int from)
        {
            return Enumerable.Empty<Vector2Int>();
        }
        public IEnumerable<Vector2Int> RangeOfMovement(Vector2Int from, MovementRate rate)
        {
            var availableCoordinates = new List<Vector2Int>();
            availableCoordinates.Add(from);
            for(int i = 0; i < rate; i++)
            {
                var currentRangeCoordinates = new List<Vector2Int>();
                foreach(var coordinates in availableCoordinates)
                {
                    currentRangeCoordinates.AddRange(AdjacentsOf(coordinates));
                }
                availableCoordinates.AddRange(currentRangeCoordinates.Where(x => !availableCoordinates.Contains(x)));
            }

            availableCoordinates.Remove(from);
            return availableCoordinates;
        }
        
        [Pure] public IEnumerable<Vector2Int> AdjacentsOf(Vector2Int coord)
        {
            return coord.CoordsAdjacentsOf().Where(InsideBounds);
        }

        bool InsideBounds(Vector2Int coord)
        {
            return coord.x >= 0 && coord.x < SizeX && coord.y >= 0 && coord.y < SizeY;
        }


    }

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