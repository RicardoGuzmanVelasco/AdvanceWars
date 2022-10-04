using System.Linq;
using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;
using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public class RecruitManeuver : SpawnerManeuver
    {
        private Unit Unit { get; }

        protected internal RecruitManeuver([NotNull] Spawner spawner, [NotNull] Unit unit)
            : base(spawner, Tactic.Recruit)
        {
            Unit = unit;
        }

        public override void Apply(Map.Map map)
        {
            var performerSpace = map.WhereIs(Performer);
            var terrain = performerSpace!.Terrain;
            
            Require(terrain.SpawnableUnits.Contains(Unit)).True();
            
            performerSpace!.SpawnHere(Unit);
        }
    }
}