using AdvanceWars.Runtime;

namespace AdvanceWars.Tests.Builders
{
    internal class UnitBuilder
    {
        MovementRate mobility = 0;
        Propulsion propulsion = new Propulsion("");
        Armor armor = new Armor("");
        Weapon weapon = WeaponBuilder.Weapon().Build();
        int minRange = 1;
        int maxRange = 1;
        #region ObjectMothers
        public static UnitBuilder Unit()
        {
            return new UnitBuilder();
        }
        #endregion

        #region FluentAPI
        
        public UnitBuilder WithRange(int minRange, int maxRange)
        {
            this.minRange = minRange;
            this.maxRange = maxRange;
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
        #endregion

        public Unit Build()
        {
            return new Unit
            {
                Mobility = mobility,
                Armor = armor,
                Propulsion = propulsion,
                Weapon = weapon,
                MinRange = minRange,
                MaxRange = maxRange
            };
        }
    }
}