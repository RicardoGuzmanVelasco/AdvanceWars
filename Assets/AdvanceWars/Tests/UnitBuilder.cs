using AdvanceWars.Runtime;

namespace AdvanceWars.Tests
{
    public class UnitBuilder
    {
        string nationId = "";
        int movementRate = 1;

        public static UnitBuilder Unit() => new UnitBuilder();
        public static UnitBuilder Infantry() => new UnitBuilder { movementRate = 3 };

        public UnitBuilder Friend() => WithNation("Friend");
        public UnitBuilder Enemy() => WithNation("Enemy");

        public UnitBuilder WithNation(string nationId)
        {
            this.nationId = nationId;
            return this;
        }

        public UnitBuilder WithMovementRate(int movementRate)
        {
            this.movementRate = movementRate;
            return this;
        }

        public Unit Build()
        {
            return new Unit
            {
                Motherland = new Nation(nationId),
                MovementRate = movementRate
            };
        }
    }
}