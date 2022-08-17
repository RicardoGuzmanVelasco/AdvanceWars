using System.Collections.Generic;
using AdvanceWars.Runtime;

namespace AdvanceWars.Tests
{
    internal class TerrainBuilder
    {
        int defense = 0;
        readonly Dictionary<Propulsion, int> costs = new Dictionary<Propulsion, int>();
        readonly List<Propulsion> blocked = new List<Propulsion>();

        #region ObjectMothers
        public static TerrainBuilder Terrain()
        {
            return new TerrainBuilder();
        }
        #endregion

        #region FluentAPI
        public TerrainBuilder WithDefense(int rating)
        {
            defense = rating;
            return this;
        }

        public TerrainBuilder WithCost(Propulsion propulsion, int cost)
        {
            costs.Add(propulsion, cost);
            return this;
        }

        public TerrainBuilder WithBlocked(params Propulsion[] propulsion)
        {
            blocked.AddRange(propulsion);
            return this;
        }
        #endregion

        public Terrain Build()
        {
            return new Terrain(costs, blocked)
            {
                DefensiveRating = defense,
            };
        }
    }
}