using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain.Fire
{
    public class TheaterOps
    {
        public Battalion Battalion { get; }
        public Terrain Battlefield { get; }

        public TheaterOps(Terrain battlefield, Battalion battalion)
        {
            Require(battlefield.Equals(Terrain.Null)).False();
            Require(battalion is INull).False();

            Battlefield = battalion.IsAerial() ? Terrain.Air : battlefield;
            Battalion = battalion;
        }
    }
}