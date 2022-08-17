using System.Collections.Generic;
using AdvanceWars.Runtime;

namespace AdvanceWars.Tests.Builders
{
    internal class WeaponBuilder
    {
        readonly Dictionary<Unit, int> damages = new Dictionary<Unit, int>();

        #region ObjectMothers
        public static WeaponBuilder Weapon() => new WeaponBuilder();
        #endregion

        #region FluentAPI
        public WeaponBuilder WithDamage(Unit unit, int damage)
        {
            damages[unit] = damage;
            return this;
        }
        #endregion

        public Weapon Build()
        {
            return new Weapon(damages);
        }
    }
}