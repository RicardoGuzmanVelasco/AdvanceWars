using System.Linq;
using AdvanceWars.Runtime.Domain.Troops;
using JetBrains.Annotations;
using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Orders.Maneuvers
{
    public class RecruitManeuver : SpawnerManeuver
    {
        Unit Unit { get; }
        Treasury Treasury { get; }
        
        protected internal RecruitManeuver([NotNull] Spawner spawner, [NotNull] Unit unit, [NotNull] Treasury treasury)
            : base(spawner, Tactic.Recruit)
        {
            Unit = unit;
            Treasury = treasury;
        }

        public override void Apply(Situation situation)
        {
            Require(Treasury.CanAfford(Unit));

            var performerSpace = situation.WhereIs(Performer);
            var terrain = performerSpace!.Terrain;
            
            Require(terrain.SpawnableUnits.Contains(Unit)).True();
            
            performerSpace!.SpawnHere(Unit);
            Treasury.PayRecruitment(Unit);
        }
    }
}