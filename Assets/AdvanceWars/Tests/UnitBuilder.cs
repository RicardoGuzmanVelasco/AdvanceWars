using AdvanceWars.Runtime;

namespace AdvanceWars.Tests
{
    internal class UnitBuilder
    {
        MovementRate mobility = 0;
        Propulsion propulsion = new Propulsion("");
        Weapon weapon = WeaponBuilder.Weapon().Build();

        #region ObjectMothers
        public static UnitBuilder Unit()
        {
            return new UnitBuilder();
        }
        #endregion

        #region FluentAPI
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
        #endregion

        public Unit Build()
        {
            return new Unit
            {
                Mobility = mobility,
                Propulsion = propulsion,
                Weapon = weapon
            };
        }
    }
}