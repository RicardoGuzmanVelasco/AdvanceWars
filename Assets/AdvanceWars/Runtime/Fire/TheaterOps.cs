using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class TheaterOps
    {
        public Battalion Troops { get; }
        public Terrain Battlefield { get; }

        public TheaterOps(Terrain battlefield, Battalion troop)
        {
            Require(battlefield.Equals(Terrain.Null)).False();
            Require(troop is INull).False();

            Battlefield = troop.IsAerial() ? Terrain.Air : battlefield;
            Troops = troop;
        }
    }
}