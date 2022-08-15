using JetBrains.Annotations;
using UnityEngine;
using static RGV.DesignByContract.Runtime.Precondition;

namespace AdvanceWars.Runtime
{
    //el hecho de disparar. 
    public class Offensive
    {
        readonly Batallion attacker;
        readonly Batallion defender;
        readonly Terrain battlefield;

        public Offensive([NotNull] Batallion attacker, [NotNull] Batallion defender, Terrain battlefield = null)
        {
            Require(attacker.Equals(defender)).False();
            battlefield ??= Terrain.Null;

            this.battlefield = battlefield;
            this.attacker = attacker;
            this.defender = defender;
        }

        public float Effectivity => attacker.Platoons / 10f;
        public float DamageReductionMultiplier => (100 - defender.Platoons * battlefield.DefensiveRating) / 100f;

        public int Damage()
        {
            return Mathf.RoundToInt
            (
                attacker.BaseDamageTo(defender.Unit) *
                Effectivity *
                DamageReductionMultiplier
            );
        }
    }
}