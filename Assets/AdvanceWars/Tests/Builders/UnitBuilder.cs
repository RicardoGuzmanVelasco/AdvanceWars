using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Tests.Builders
{
    internal class UnitBuilder
    {
        MovementRate mobility = MovementRate.None;
        Propulsion propulsion = new("");
        Armor armor = new("");
        Weapon weapon = Weapon.Null;
        RangeOfFire rangeOfFire = RangeOfFire.One;
        Military serviceBranch = Military.None;
        Price price = Price.Free;

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

        public UnitBuilder WithPrice(Price price)
        {
            this.price = price;
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

        public UnitBuilder Of(Military branch)
        {
            this.serviceBranch = branch;
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
                ServiceBranch = serviceBranch,
                Weapon = weapon,
                RangeOfFire = rangeOfFire,
                Price = price
            };
        }

    }
}