using System.Collections.Generic;
using System.Linq;
using static RGV.DesignByContract.Runtime.Precondition;

namespace AdvanceWars.Runtime
{
    public class Terrain
    {
        readonly Dictionary<Propulsion, int> costs = new Dictionary<Propulsion, int>();

        public Terrain(Dictionary<Propulsion, int> costs)
            : this(costs, Enumerable.Empty<Propulsion>()) { }

        public Terrain(IEnumerable<Propulsion> blocked)
            : this(new Dictionary<Propulsion, int>(), blocked) { }

        public Terrain(Dictionary<Propulsion, int> costs, IEnumerable<Propulsion> blocked)
        {
            Require(costs.Keys.Intersect(blocked).Any()).False();
            Require(costs.Values.All(x => x >= 0)).True();

            this.costs = new Dictionary<Propulsion, int>(costs);
            foreach(var propulsion in blocked)
                this.costs[propulsion] = int.MaxValue;
        }

        public int MoveCostOf(Propulsion propulsion)
        {
            return costs.ContainsKey(propulsion) ? costs[propulsion] : 1;
        }

        public static Terrain Null { get; } = new Terrain(new Dictionary<Propulsion, int>());
    }
}