using AdvanceWars.Runtime.Domain.Troops;
using static AdvanceWars.Tests.Builders.UnitBuilder;

namespace AdvanceWars.Tests.Builders
{
    internal class BattalionBuilder
    {
        Nation nation = Nation.Stateless;
        int forces = 100;
        int ammoRounds = 10;

        UnitBuilder fromUnit = Unit();

        #region ObjectMothers
        public static BattalionBuilder Battalion() => new();
        public static BattalionBuilder Infantry() => new() { fromUnit = Unit().WithMobility(3).Of(Military.Army) };
        public static BattalionBuilder AerialUnit() => new() { fromUnit = Unit().Of(Military.AirForce) };
        #endregion

        #region Fluent API
        public BattalionBuilder Ally() => WithNation("IsAlly");
        public BattalionBuilder Enemy() => WithNation("IsEnemy");

        public BattalionBuilder Of(UnitBuilder unitBuilder)
        {
            fromUnit = unitBuilder;
            return this;
        }

        public BattalionBuilder WithNation(string id)
        {
            nation = new Nation(id);
            return this;
        }

        public BattalionBuilder WithNation(Nation nation)
        {
            this.nation = nation;
            return this;
        }

        public BattalionBuilder WithMoveRate(int movementRate)
        {
            fromUnit.WithMobility(movementRate);
            return this;
        }

        public BattalionBuilder WithPrice(Price price)
        {
            fromUnit.With(price);
            return this;
            
        }
        public BattalionBuilder WithArmor(string armorId)
        {
            fromUnit.With(new Armor(armorId));
            return this;
        }

        public BattalionBuilder WithPropulsion(Propulsion propulsion)
        {
            fromUnit.With(propulsion);
            return this;
        }

        public BattalionBuilder WithForces(int count)
        {
            forces = count;
            return this;
        }

        public BattalionBuilder WithMaxForces()
        {
            forces = Runtime.Domain.Troops.Battalion.MaxForces;
            return this;
        }

        public BattalionBuilder WithPlatoons(int count)
        {
            forces = 10 * count;
            return this;
        }

        public BattalionBuilder WithWeapon(Weapon weapon)
        {
            fromUnit.With(weapon);
            return this;
        }

        public BattalionBuilder WithRange(int minRange, int maxRange)
        {
            fromUnit.WithFire(minRange, maxRange);
            return this;
        }

        public BattalionBuilder WithAmmo(int rounds)
        {
            ammoRounds = rounds;
            return this;
        }
        #endregion

        public Battalion Build()
        {
            return new Battalion
            {
                Unit = fromUnit.Build(),
                Forces = forces,
                AmmoRounds = ammoRounds,
                Motherland = nation
            };
        }
    }
}