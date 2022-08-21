using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime.DataStructures;
using JetBrains.Annotations;
using UnityEngine;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public partial record Map(int SizeX, int SizeY)
    {
        readonly LazyBoard<Space> spaces = new LazyBoard<Space>();

        public void Put(Vector2Int coord, Batallion batallion)
        {
            spaces[coord].Occupant = batallion;
        }

        public void Put(Vector2Int coord, Terrain terrain)
        {
            spaces[coord].Terrain = terrain;
        }

        [NotNull]
        public IEnumerable<Vector2Int> RangeOfMovement(Batallion batallion)
        {
            Require(WhereIs(batallion)).Not.Null();
            return RangeOfMovement(from: CoordOf(WhereIs(batallion)), rate: batallion.MovementRate);
        }

        [NotNull]
        public IEnumerable<Vector2Int> RangeOfMovement(Vector2Int from, MovementRate rate)
        {
            Require(InsideBounds(from)).True();

            var targetBatallion = spaces[from].Occupant;

            var availableCoords = new List<Vector2Int>();
            availableCoords.Add(from);
            for(int i = 0; i < rate; i++)
            {
                var currentRangeCoords = new List<Vector2Int>();
                foreach(var coords in availableCoords)
                {
                    //Esto es un mockeo para que solo devuelva vacío en cuanto haya un bloqueo de la propulsión.
                    var isBlocker = spaces[coords].Terrain.MoveCostOf(targetBatallion.Propulsion) == int.MaxValue;
                    if(isBlocker)
                        return Enumerable.Empty<Vector2Int>();
                    currentRangeCoords.AddRange(AdjacentsOf(coords)
                        .Where(c => spaces[c].IsCrossableBy(targetBatallion)));
                }

                availableCoords.AddRange(currentRangeCoords.Where(x => !availableCoords.Contains(x)));
            }

            availableCoords.Remove(from);
            return availableCoords.Where(c => !spaces[c].IsOccupied);
        }

        [Pure, NotNull]
        IEnumerable<Vector2Int> AdjacentsOf(Vector2Int coord)
        {
            Require(InsideBounds(coord)).True();
            return coord.CoordsAdjacentsOf().Where(InsideBounds);
        }

        bool InsideBounds(Vector2Int coord)
        {
            return coord.x >= 0 &&
                   coord.x < SizeX &&
                   coord.y >= 0 &&
                   coord.y < SizeY;
        }

        [CanBeNull]
        Space WhereIs(Batallion batallion)
        {
            return spaces.Values.SingleOrDefault(x => x.Occupant == batallion);
        }

        Vector2Int CoordOf([NotNull] Space space)
        {
            return spaces.CoordsOf(space);
        }
    }
}