using System.Collections.Generic;
using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Tests.Builders
{
    internal class WeaponBuilder
    {
        readonly Dictionary<Armor, int> damages = new Dictionary<Armor, int>();

        #region ObjectMothers
        public static WeaponBuilder Weapon() => new WeaponBuilder();
        #endregion

        #region FluentAPI
        public WeaponBuilder WithDamage(Armor armor, int damage)
        {
            damages[armor] = damage;
            return this;
        }

        public WeaponBuilder MaxDmgTo(string armorId)
        {
            damages[new Armor(armorId)] = int.MaxValue;
            return this;
        }
        #endregion

        public Weapon Build()
        {
            return new Weapon(damages);
        }
    }
}