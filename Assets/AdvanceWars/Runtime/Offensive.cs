using JetBrains.Annotations;
using UnityEngine;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Offensive
    {
        readonly Battalion attacker;
        readonly Battalion defender;
        readonly Terrain battlefield;

        public Offensive([NotNull] Battalion attacker, [NotNull] Battalion defender, Terrain battlefield = null)
        {
            Require(attacker.Equals(defender)).False();
            battlefield ??= Terrain.Null;

            this.battlefield = battlefield;
            this.attacker = attacker;
            this.defender = defender;
        }

        public float Effectivity => attacker.Platoons / 10f;
        public float DamageReductionMultiplier => (100 - defender.Platoons * battlefield.DefensiveRating) / 100f;

        public int Damage =>
            Mathf.RoundToInt
            (
                attacker.BaseDamageTo(defender.Unit.Armor) *
                Effectivity *
                DamageReductionMultiplier
            );

        public Battalion Outcome()
        {
            var resultForces = defender.Forces - Damage;
            if(resultForces <= 0)
                return Battalion.Null;

            var result = defender.Clone();
            result.Forces = resultForces;

            return result;
        }
    }
}