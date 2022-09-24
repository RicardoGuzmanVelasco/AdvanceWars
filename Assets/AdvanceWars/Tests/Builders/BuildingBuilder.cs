using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Tests.Builders
{
    internal class BuildingBuilder
    {
        int siegePoints = 0;
        Nation owner = Nation.Stateless;

        public static BuildingBuilder Building()
        {
            return new BuildingBuilder();
        }

        public BuildingBuilder WithPoints(int siegePoints)
        {
            this.siegePoints = siegePoints;
            return this;
        }

        public BuildingBuilder WithOwner(string ownerId) => WithOwner(new Nation(ownerId));

        public BuildingBuilder WithOwner(Nation owner)
        {
            this.owner = owner;
            return this;
        }

        public Building Build()
        {
            return new Building(siegePoints, owner);
        }
    }
}