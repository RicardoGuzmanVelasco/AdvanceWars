using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public record Unit
    {
        public MovementRate Mobility { get; init; }
        public Propulsion Propulsion { get; init; }
        public Weapon Weapon { get; init; }

        public Armor Armor { get; init; }

        public int BaseDamageTo(Armor other)
        {
            Require(other).Not.Null();
            return Weapon.BaseDamageTo(other);
        }
    }
}