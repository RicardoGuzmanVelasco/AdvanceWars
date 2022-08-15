using AdvanceWars.Runtime;

namespace AdvanceWars.Tests
{
    public class BatallionBuilder
    {
        string nationId = "";
        int movementRate = 1;
        string propulsionId = "";
        int strength = 100;

        public static BatallionBuilder Batallion() => new BatallionBuilder();
        public static BatallionBuilder Infantry() => new BatallionBuilder { movementRate = 3 };

        public BatallionBuilder Friend() => WithNation("Friend");
        public BatallionBuilder Enemy() => WithNation("Enemy");

        public BatallionBuilder WithNation(string nationId)
        {
            this.nationId = nationId;
            return this;
        }

        public BatallionBuilder WithMoveRate(int movementRate)
        {
            this.movementRate = movementRate;
            return this;
        }

        public BatallionBuilder WithPropulsion(string propulsionId)
        {
            this.propulsionId = propulsionId;
            return this;
        }

        public BatallionBuilder WithStrength(int strength)
        {
            this.strength = strength;
            return this;
        }

        public Batallion Build()
        {
            return new Batallion
            {
                AllegianceTo = new Nation(nationId),
                Unit = new Unit
                {
                    Mobility = movementRate,
                    Propulsion = new Propulsion(propulsionId),
                },
                Strength = strength
            };
        }
    }
}