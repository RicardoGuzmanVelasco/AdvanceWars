using AdvanceWars.Runtime;

namespace AdvanceWars.Tests.Builders
{
    internal class BattalionBuilder
    {
        string nationId = "";
        int forces = 100;

        Unit fromUnit = new Unit //Reuse unit builder.
        {
            Mobility = 1,
            Propulsion = new Propulsion(""),
            Weapon = null
        };

        #region ObjectMothers
        public static BattalionBuilder Battalion() => new BattalionBuilder();
        public static BattalionBuilder Infantry() => new BattalionBuilder { fromUnit = new Unit { Mobility = 3 } };
        #endregion

        #region Fluent API
        public BattalionBuilder Friend() => WithNation("Friend");
        public BattalionBuilder Enemy() => WithNation("Enemy");

        public BattalionBuilder Of(Unit unit)
        {
            fromUnit = unit;
            return this;
        }

        public BattalionBuilder WithNation(string id)
        {
            nationId = id;
            return this;
        }

        public BattalionBuilder WithMoveRate(int movementRate)
        {
            fromUnit = fromUnit with { Mobility = movementRate };
            return this;
        }

        public BattalionBuilder WithPropulsion(string propulsionId)
        {
            fromUnit = fromUnit with { Propulsion = new Propulsion(propulsionId) };
            return this;
        }

        public BattalionBuilder WithPropulsion(Propulsion propulsion)
        {
            fromUnit = fromUnit with { Propulsion = propulsion };
            return this;
        }

        public BattalionBuilder WithForces(int count)
        {
            forces = count;
            return this;
        }

        public BattalionBuilder WithWeapon(Weapon weapon)
        {
            fromUnit = fromUnit with { Weapon = weapon };
            return this;
        }
        #endregion

        public Battalion Build()
        {
            return new Battalion
            {
                AllegianceTo = new Nation(nationId),
                Unit = fromUnit,
                Forces = forces
            };
        }
    }
}