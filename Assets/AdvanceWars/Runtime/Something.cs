using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Precondition;

namespace AdvanceWars.Runtime
{
    //el hecho de disparar. 
    public class Something
    {
        readonly Batallion attacker;
        readonly Batallion defender;
        readonly Terrain battlefield;

        public Something([NotNull] Batallion attacker, [NotNull] Batallion defender, Terrain battlefield = null)
        {
            Require(attacker.Equals(defender)).False();
            battlefield ??= Terrain.Null;

            this.battlefield = battlefield;
            this.attacker = attacker;
            this.defender = defender;
        }

        public float OffensiveEffectivity => attacker.Platoons / 10f;
        public float DamageReductionMultiplier => (100 - defender.Platoons * battlefield.DefensiveRating) / 100f;
    }
}