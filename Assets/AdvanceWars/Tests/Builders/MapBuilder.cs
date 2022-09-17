using AdvanceWars.Runtime;

namespace AdvanceWars.Tests.Builders
{
    public class MapBuilder
    {
        int sizeX;
        int sizeY;

        #region ObjectMothers
        public static MapBuilder Map() => new MapBuilder();
        #endregion

        public MapBuilder Of(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;

            return this;
        }

        public MapBuilder Of(int squaredSize)
        {
            Of(squaredSize, squaredSize);
            return this;
        }

        public Map Build()
        {
            return new Map(this.sizeX, this.sizeY);
        }
    }
}