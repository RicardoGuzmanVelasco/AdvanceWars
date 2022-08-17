﻿using AdvanceWars.Runtime;

namespace AdvanceWars.Tests
{
    public class BatallionBuilder
    {
        string nationId = "";
        int forces = 100;

        Unit fromUnit = new Unit
        {
            Mobility = 1,
            Propulsion = new Propulsion(""),
            Weapon = null
        };

        #region ObjectMothers
        public static BatallionBuilder Batallion() => new BatallionBuilder();
        public static BatallionBuilder Infantry() => new BatallionBuilder { fromUnit = new Unit { Mobility = 3 } };

        public BatallionBuilder Friend() => WithNation("Friend");
        public BatallionBuilder Enemy() => WithNation("Enemy");
        #endregion

        #region Fluent API
        public BatallionBuilder WithNation(string id)
        {
            nationId = id;
            return this;
        }

        public BatallionBuilder WithMoveRate(int movementRate)
        {
            fromUnit = fromUnit with { Mobility = movementRate };
            return this;
        }

        public BatallionBuilder WithPropulsion(string propulsionId)
        {
            fromUnit = fromUnit with { Propulsion = new Propulsion(propulsionId) };
            return this;
        }

        public BatallionBuilder WithForces(int count)
        {
            forces = count;
            return this;
        }

        public BatallionBuilder WithWeapon(Weapon weapon)
        {
            fromUnit = fromUnit with { Weapon = weapon };
            return this;
        }
        #endregion

        public Batallion Build()
        {
            return new Batallion
            {
                AllegianceTo = new Nation(nationId),
                Unit = fromUnit,
                Forces = forces
            };
        }
    }
}