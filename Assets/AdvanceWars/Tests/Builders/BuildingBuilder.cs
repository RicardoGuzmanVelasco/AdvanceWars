using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Tests.Builders
{
    internal class BuildingBuilder
    {
        int siegePoints = 0;
        Nation owner = Nation.Stateless;
        private int income;

        public static BuildingBuilder Building()
        {
            return new BuildingBuilder();
        }

        public BuildingBuilder WithPoints(int siegePoints)
        {
            this.siegePoints = siegePoints;
            return this;
        }

        public BuildingBuilder WithNation(string ownerId) => WithNation(new Nation(ownerId));

        public BuildingBuilder WithNation(Nation owner)
        {
            this.owner = owner;
            return this;
        }

        public BuildingBuilder WithIncome(int amount)
        {
            income = amount;
            return this;
        }
        
        public Building Build()
        {
            return new Building(siegePoints, owner, income);
        }
    }
}