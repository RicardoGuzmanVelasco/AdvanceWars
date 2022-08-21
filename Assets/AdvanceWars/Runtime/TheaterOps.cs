using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class TheaterOps
    {
        public Batallion Troops { get; }
        public Terrain Battlefield { get; }

        public TheaterOps(Terrain battlefield, Batallion troop)
        {
            Require(battlefield.Equals(Terrain.Null)).False();
            Require(troop.Equals(Batallion.Null)).False();

            Battlefield = battlefield;
            Troops = troop;
        }
    }
}