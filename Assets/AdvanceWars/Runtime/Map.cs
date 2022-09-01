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
        private IEnumerable<Vector2Int> newCoords;

        public void Put(Vector2Int coord, Battalion battalion)
        {
            spaces[coord].Occupy(battalion);
        }

        public void Put(Vector2Int coord, Terrain terrain)
        {
            spaces[coord].Terrain = terrain;
        }

        [NotNull]
        public IEnumerable<Vector2Int> RangeOfMovement(Battalion battalion)
        {
            Require(WhereIs(battalion)).Not.Null();
            return RangeOfMovement(from: CoordOf(WhereIs(battalion)), rate: battalion.MovementRate);
        }

        [NotNull]
        public IEnumerable<Vector2Int> RangeOfMovement(Vector2Int from, MovementRate rate)
        {
            Require(InsideBounds(from)).True();

            var targetBattalion = spaces[from].Occupant;

            var availableCoords = new List<Vector2Int>();
            availableCoords.Add(from);
            for(int i = 0; i < rate; i++)
            {
                var currentRangeCoords = new List<Vector2Int>();
                foreach(var coords in availableCoords)
                {
                    //Esto es un mockeo para que solo devuelva vacío en cuanto haya un bloqueo de la propulsión.
                    var isBlocker = spaces[coords].Terrain.MoveCostOf(targetBattalion.Propulsion) == int.MaxValue;
                    if(isBlocker)
                        return Enumerable.Empty<Vector2Int>();
                    currentRangeCoords.AddRange(AdjacentsOf(coords)
                        .Where(c => spaces[c].IsCrossableBy(targetBattalion)));
                }

                availableCoords.AddRange(currentRangeCoords.Where(x => !availableCoords.Contains(x)));
            }

            availableCoords.Remove(from);
            return availableCoords.Where(c => !spaces[c].IsOccupied);
        }

        public Space OfCoords(Vector2Int coords)
        {
            return spaces[coords];
        }
        
        public virtual IEnumerable<Battalion> EnemyBattalionsInRangeOfFire(Battalion battalion)
        {
            var coordsInRange = RangeOfFire(battalion);
            return spaces.Where(x => coordsInRange.Contains(x.Key) && x.Value.Occupant.IsEnemy(battalion))
                .Select(x => x.Value.Occupant);
        }
        
        public IEnumerable<Vector2Int> RangeOfFire(Battalion battalion)
        {
            return RangeOfFire(CoordOf(WhereIs(battalion)!), battalion.MinRange, battalion.MaxRange);
        }
        
        public IEnumerable<Vector2Int> RangeOfFire(Vector2Int from, int maxRange)
        {
            return RangeOfFire(from, 0, maxRange);
        }

        public IEnumerable<Vector2Int> RangeOfFire(Vector2Int from, int minRange, int maxRange)
        {
            Require(minRange <= maxRange).True();
            Require(maxRange >= 1).True();
            
            var coordsOutsideMinRange = CoordsInsideRange(from, minRange - 1);

            var coordsInsideMaxRange = CoordsInsideRange(from, maxRange);

            return coordsInsideMaxRange.Where(x => x != from && !coordsOutsideMinRange.Contains(x));
        }

        private IEnumerable<Vector2Int> CoordsInsideRange(Vector2Int from, int range)
        {
            var coordsInsideRange = new List<Vector2Int> {from};

            for (int i = 0; i < range; i++)
            {
                var currentRangeCoords = new List<Vector2Int>();
                foreach (var coords in coordsInsideRange)
                {
                    currentRangeCoords.AddRange(AdjacentsOf(coords));
                }

                newCoords = currentRangeCoords.Where(x => !coordsInsideRange.Contains(x));
                
                coordsInsideRange.AddRange(newCoords);
            }

            return coordsInsideRange;
        }

        [Pure, NotNull]
        IEnumerable<Vector2Int> AdjacentsOf(Vector2Int coord)
        {
            Require(InsideBounds(coord)).True();
            return coord.AdjacentsCoords().Where(InsideBounds);
        }

        bool InsideBounds(Vector2Int coord)
        {
            return coord.x >= 0 &&
                   coord.x < SizeX &&
                   coord.y >= 0 &&
                   coord.y < SizeY;
        }

        [CanBeNull]
        public virtual Space WhereIs(Allegiance what)
        {
            return spaces.Values.SingleOrDefault(x => x.Occupant == what);
        }

        Vector2Int CoordOf([NotNull] Space space)
        {
            return spaces.CoordsOf(space);
        }
    }
}