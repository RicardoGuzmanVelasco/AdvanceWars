using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using static RGV.DesignByContract.Runtime.Precondition;

namespace AdvanceWars.Runtime
{
    public partial record Map(int SizeX, int SizeY)
    {
        readonly Dictionary<Vector2Int, Space> spaces = new Dictionary<Vector2Int, Space>();

        public void Put(Vector2Int coord, Batallion batallion)
        {
            CreateSpace(coord);
            spaces[coord].Occupant = batallion;
        }

        public void Put(Vector2Int coord, Terrain terrain)
        {
            CreateSpace(coord);
            spaces[coord].Terrain = terrain;
        }

        private void CreateSpace(Vector2Int coord)
        {
            Require(InsideBounds(coord)).True();

            if(!spaces.ContainsKey(coord))
                spaces.Add(coord, new Space());
        }

        public IEnumerable<Vector2Int> RangeOfMovement(Batallion batallion)
        {
            Require(WhereIs(batallion)).Not.Null();
            return RangeOfMovement(@from: CoordOf(WhereIs(batallion)), rate: batallion.MovementRate);
        }

        public IEnumerable<Vector2Int> RangeOfMovement(Vector2Int from, MovementRate rate)
        {
            Require(InsideBounds(from)).True();

            var targetBatallion = BatallionIn(from);

            var availableCoords = new List<Vector2Int>();
            availableCoords.Add(from);
            for(int i = 0; i < rate; i++)
            {
                var currentRangeCoords = new List<Vector2Int>();
                foreach(var coords in availableCoords)
                {
                    //Esto es un mockeo para que solo devuelva vacío en cuanto haya un bloqueo de la propulsión.
                    var isBlocker = TerrainIn(coords).MoveCostOf(targetBatallion.Propulsion) == int.MaxValue;
                    if(isBlocker)
                        return Enumerable.Empty<Vector2Int>();
                    currentRangeCoords.AddRange(AdjacentsOf(coords)
                        .Where(c => !spaces.ContainsKey(c) || spaces[c].IsCrossableBy(targetBatallion)));
                }

                availableCoords.AddRange(currentRangeCoords.Where(x => !availableCoords.Contains(x)));
            }

            availableCoords.Remove(from);
            return availableCoords.Where(c => !spaces.ContainsKey(c) || !spaces[c].IsOccupied);
        }

        Batallion BatallionIn(Vector2Int coord)
        {
            return spaces.ContainsKey(coord)
                ? spaces[coord].Occupant
                : Batallion.Null;
        }

        Terrain TerrainIn(Vector2Int coord)
        {
            return spaces.ContainsKey(coord)
                ? spaces[coord].Terrain
                : Terrain.Null;
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

        Space WhereIs(Batallion batallion)
        {
            return spaces.Values.SingleOrDefault(x => x.Occupant == batallion);
        }

        Vector2Int CoordOf(Space space)
        {
            return spaces.Single(x => x.Value == space).Key;
        }
    }
}