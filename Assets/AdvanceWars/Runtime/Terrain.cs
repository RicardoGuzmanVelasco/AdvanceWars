using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Terrain : Allegiance
    {
        readonly Dictionary<Propulsion, int> costs = new Dictionary<Propulsion, int>();

        public int DefensiveRating { get; init; }

        #region Ctors /FactoryMethods
        protected Terrain() { }

        Terrain(Dictionary<Propulsion, int> costs)
            : this(costs, Enumerable.Empty<Propulsion>()) { }

        public Terrain(Dictionary<Propulsion, int> costs, IEnumerable<Propulsion> blocked)
        {
            Require(costs.Keys.Intersect(blocked).Any()).False();
            Require(costs.Values.All(x => x >= 0)).True();

            this.costs = new Dictionary<Propulsion, int>(costs);

            foreach(var propulsion in blocked)
                this.costs[propulsion] = int.MaxValue;
        }

        public static Terrain Null { get; } = new Terrain(new Dictionary<Propulsion, int>());
        #endregion

        public int MoveCostOf(Propulsion propulsion)
        {
            return costs.ContainsKey(propulsion) ? costs[propulsion] : 1;
        }

        public int SiegePoints { get; set; } = int.MaxValue;
        protected int MaxSiegePoints { get; init; }

        [Pure]
        public virtual Building SiegeOutcome([NotNull] Battalion besieger)
        {
            return Building.Unbesiegable;
        }

        public virtual void LiftSiege() { }
    }
}