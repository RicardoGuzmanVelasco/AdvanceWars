using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime.DataStructures;
using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;
using UnityEngine;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Map
{
    public partial record Map(int SizeX, int SizeY) : IEnumerable<KeyValuePair<Vector2Int, Map.Space>>
    {
        readonly LazyBoard<Space> spaces = new();

        public void Put(Vector2Int coord, Battalion battalion)
        {
            spaces[coord].Occupy(battalion);
        }

        public void Put(Vector2Int coord, Terrain terrain)
        {
            spaces[coord].Terrain = terrain;
        }

        public Space SpaceAt(Vector2Int coords)
        {
            return spaces[coords];
        }

        [NotNull]
        public virtual IEnumerable<Vector2Int> RangeOfMovement(Battalion battalion)
        {
            Require(WhereIs(battalion)).Not.Null();
            return RangeOfMovement(from: CoordOf(WhereIs(battalion)!), rate: battalion.MovementRate);
        }

        [NotNull]
        public virtual IEnumerable<Vector2Int> RangeOfMovement(Vector2Int from, MovementRate rate)
        {
            Require(IsInsideBounds(from)).True();

            var targetBattalion = spaces[from].Occupant;

            var nodes = new Dictionary<Vector2Int, int>();
            nodes.Add(from, 0);

            for(int i = 0; i < rate; i++)
            {
                var adjacentNodes = new Dictionary<Vector2Int, int>();

                foreach(var node in nodes)
                {
                    int accumulatedCost = node.Value;

                    var adjacents = AdjacentsOf(node.Key);

                    foreach(var adjacent in adjacents)
                        if(spaces[adjacent].IsCrossableBy(targetBattalion))
                        {
                            var adjacentCost = accumulatedCost + spaces[adjacent].MoveCostOf(targetBattalion);
                            if(rate >= adjacentCost)
                            {
                                if(!adjacentNodes.ContainsKey(adjacent))
                                    adjacentNodes.Add(adjacent, adjacentCost);
                                else if(adjacentNodes[adjacent] > adjacentCost)
                                    adjacentNodes[adjacent] = adjacentCost;
                            }
                        }
                }

                foreach(var adjacentNode in adjacentNodes)
                    if(!nodes.ContainsKey(adjacentNode.Key))
                        nodes.Add(adjacentNode.Key, adjacentNode.Value);
                    else if(nodes[adjacentNode.Key] > adjacentNode.Value)
                        nodes[adjacentNode.Key] = adjacentNode.Value;
            }

            nodes.Remove(from);
            var coords = nodes.Keys.Where(c => spaces[c].CanEnter(targetBattalion));

            return coords;
        }

        public virtual IEnumerable<Battalion> EnemyBattalionsInRangeOfFire(Battalion battalion)
        {
            var coordsInRange = RangeOfFire(battalion);
            return spaces.Where(x => coordsInRange.Contains(x.Key) && x.Value.Occupant.IsEnemy(battalion))
                .Select(x => x.Value.Occupant);
        }

        public IEnumerable<Vector2Int> RangeOfFire(Battalion battalion)
        {
            return RangeOfFire(CoordOf(WhereIs(battalion)!), battalion.RangeOfFire);
        }

        public IEnumerable<Vector2Int> RangeOfFire(Vector2Int from, int maxRange)
        {
            return RangeOfFire(from, new RangeOfFire(1, maxRange));
        }

        public IEnumerable<Vector2Int> RangeOfFire(Vector2Int from, RangeOfFire range)
        {
            var coordsOutsideMinRange = CoordsInsideRange(from, range.Min - 1);
            var coordsInsideMaxRange = CoordsInsideRange(from, range.Max);

            return coordsInsideMaxRange.Where(x => x != from && !coordsOutsideMinRange.Contains(x));
        }

        private IEnumerable<Vector2Int> CoordsInsideRange(Vector2Int from, int range)
        {
            var coordsInsideRange = new List<Vector2Int> { from };

            for(int i = 0; i < range; i++)
            {
                var currentRangeCoords = new List<Vector2Int>();
                foreach(var coords in coordsInsideRange)
                {
                    currentRangeCoords.AddRange(AdjacentsOf(coords));
                }

                var newCoords = currentRangeCoords.Where(x => !coordsInsideRange.Contains(x));

                coordsInsideRange.AddRange(newCoords);
            }

            return coordsInsideRange;
        }

        [Pure, NotNull]
        IEnumerable<Vector2Int> AdjacentsOf(Vector2Int coord)
        {
            Require(IsInsideBounds(coord)).True();
            return coord.AdjacentsCoords().Where(IsInsideBounds);
        }

        public bool IsInsideBounds(Vector2Int coord)
        {
            return coord.x >= 0 &&
                   coord.x < SizeX &&
                   coord.y >= 0 &&
                   coord.y < SizeY;
        }

        [CanBeNull]
        public virtual Space WhereIs(Terrain what)
        {
            return spaces.Values.SingleOrDefault(x => x.Terrain == what);
        }

        [CanBeNull]
        public virtual Space WhereIs(Battalion what)
        {
            return spaces.Values.SingleOrDefault(x => x.Occupant == what);
        }

        Vector2Int CoordOf([NotNull] Space space)
        {
            return spaces.CoordsOf(space);
        }

        public IEnumerable<Space> FriendlyTerrainSpaces(Allegiance other)
        {
            return spaces
                .Select(keyValuePair => keyValuePair.Value)
                .Where(space => space.Terrain.IsAlly(other));
        }

        public IEnumerable<Space> SpaceOccupiedByAlly(Allegiance other)
        {
            return spaces
                .Select(keyValuePair => keyValuePair.Value)
                .Where(space => space.Occupant.IsAlly(other));
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<Vector2Int, Space>> GetEnumerator()
        {
            return spaces.GetEnumerator();
        }
    }
}