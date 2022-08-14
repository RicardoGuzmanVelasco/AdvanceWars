using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using static RGV.DesignByContract.Runtime.Precondition;

namespace AdvanceWars.Runtime
{
    public record Map(int SizeX, int SizeY)
    {
        readonly List<Vector2Int> occupiedCoords_MOCK = new List<Vector2Int>();

        public IEnumerable<Vector2Int> RangeOfMovement(Vector2Int from, MovementRate rate)
        {
            Require(InsideBounds(from)).True();

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
            return availableCoordinates.Where(c => !occupiedCoords_MOCK.Contains(c));
        }

        [Pure, NotNull]
        IEnumerable<Vector2Int> AdjacentsOf(Vector2Int coord)
        {
            Require(InsideBounds(coord)).True();
            return coord.CoordsAdjacentsOf().Where(InsideBounds);
        }

        bool InsideBounds(Vector2Int coord)
        {
            return coord.x >= 0 && coord.x < SizeX && coord.y >= 0 && coord.y < SizeY;
        }

        public void Occupy(Vector2Int coord)
        {
            occupiedCoords_MOCK.Add(coord);
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