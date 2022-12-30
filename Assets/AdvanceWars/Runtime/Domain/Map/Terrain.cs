using System.Collections.Generic;
using System.Linq;
using AdvanceWars.Runtime.Domain.Troops;
using AdvanceWars.Runtime.Extensions.DataStructures;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Map
{
    public class Terrain : Allegiance
    {
        readonly Dictionary<Propulsion, int> costs = new();
        ZeroClampedInt siegePoints;

        public int DefensiveRating { get; init; }

        protected virtual int Income => 0;

        public string Id { get; init; } = string.Empty;

        #region Ctors /FactoryMethods

        protected Terrain(int maxSiegePoints)
        {
            siegePoints = new ZeroClampedInt(maxSiegePoints, maxSiegePoints);
        }

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

        public static Terrain Null { get; } = new(new Dictionary<Propulsion, int>());

        public static Terrain Air { get; } = new(new Dictionary<Propulsion, int>()) { Id = "Air" };
        #endregion

        public int MoveCostOf(Propulsion propulsion)
        {
            return costs.ContainsKey(propulsion) ? costs[propulsion] : 1;
        }

        #region Siege-related pushed up stuff
        public virtual bool IsUnderSiege => false;

        public ZeroClampedInt SiegePoints
        {
            get => siegePoints;
            set
            {
                Require(SiegePoints).Not.Null();
                siegePoints = new ZeroClampedInt(value, SiegePoints.Ceil);
            }
        }

        internal virtual IEnumerable<Unit> SpawnableUnits => Enumerable.Empty<Unit>();

        [Pure]
        public virtual Building SiegeOutcome([NotNull] Battalion besieger)
        {
            return Building.Unbesiegable;
        }

        public virtual void LiftSiege() { }
        #endregion

        public virtual bool IsBesiegable(Battalion besieger)
        {
            return false;
        }

        public virtual bool CanHeal(Battalion patient, Treasury treasury)
        {
            return false;
        }

        public virtual void Heal(Battalion patient, Treasury treasury) { }

        public Battalion SpawnBattalion(Unit ofUnit)
        {
            Require(SpawnableUnits).Contains(ofUnit);
            return new Battalion { Unit = ofUnit, Motherland = Motherland };
        }

        public void ReportIncome([NotNull] Treasury treasury)
        {
            if(Income > 0) 
                treasury.Earn(Income);
        }
    }
}