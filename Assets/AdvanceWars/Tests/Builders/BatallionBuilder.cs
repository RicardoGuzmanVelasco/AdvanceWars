using AdvanceWars.Runtime.Domain.Troops;
using static AdvanceWars.Tests.Builders.UnitBuilder;

namespace AdvanceWars.Tests.Builders
{
    internal class BattalionBuilder
    {
        string nationId = "";
        int forces = 100;

        UnitBuilder fromUnit = Unit();

        #region ObjectMothers
        public static BattalionBuilder Battalion() => new();
        public static BattalionBuilder Infantry() => new() { fromUnit = Unit().WithMobility(3) };
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
            nationId = id;
            return this;
        }

        public BattalionBuilder WithMoveRate(int movementRate)
        {
            fromUnit.WithMobility(movementRate);
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

        public BattalionBuilder WithInfiniteForces()
        {
            forces = int.MaxValue;
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
        #endregion

        public Battalion Build()
        {
            return new Battalion
            {
                Motherland = nationId.Equals("") ? Nation.Stateless : new Nation(nationId),
                Unit = fromUnit.Build(),
                Forces = forces
            };
        }
    }
}