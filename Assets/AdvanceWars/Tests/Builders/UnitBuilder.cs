using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Tests.Builders
{
    internal class UnitBuilder
    {
        MovementRate mobility = MovementRate.None;
        Propulsion propulsion = new("");
        Armor armor = new("");
        Weapon weapon = Weapon.Null;
        private RangeOfFire rangeOfFire = RangeOfFire.One;
        Military force = Military.None;

        #region ObjectMothers
        public static UnitBuilder Unit()
        {
            return new UnitBuilder();
        }
        #endregion

        #region FluentAPI
        public UnitBuilder WithFire(int minRange, int maxRange)
        {
            this.rangeOfFire = new RangeOfFire(minRange, maxRange);
            return this;
        }

        public UnitBuilder WithMobility(MovementRate mobility)
        {
            this.mobility = mobility;
            return this;
        }

        public UnitBuilder With(Propulsion propulsion)
        {
            this.propulsion = propulsion;
            return this;
        }

        public UnitBuilder With(Weapon weapon)
        {
            this.weapon = weapon;
            return this;
        }

        public UnitBuilder With(Armor armor)
        {
            this.armor = armor;
            return this;
        }

        public UnitBuilder Of(Military force)
        {
            this.force = force;
            return this;
        }
        #endregion

        public Unit Build()
        {
            return new Unit
            {
                Mobility = mobility,
                Armor = armor,
                Propulsion = propulsion,
                Force = force,
                Weapon = weapon,
                RangeOfFire = rangeOfFire
            };
        }
    }
}