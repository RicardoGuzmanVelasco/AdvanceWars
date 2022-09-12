using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AdvanceWars.Tests.Builders
{
    public class RangeOfBuilder
    {
        private IEnumerable<Vector2Int> range = new List<Vector2Int>();
        
        #region ObjectMothers
        public static RangeOfBuilder RangeOf() => new RangeOfBuilder();
        #endregion
        
        public RangeOfBuilder WithStructure(string mapAsVerbatimString)
        {
            var lines = mapAsVerbatimString.Split("\n");
            
            var sizeX = SpacesIn(lines.First());
            var sizeY = lines.Length;

            return this;
        }
        
        int SpacesIn(string row)
        {
            return row.Count(x => x == 'O');
        }

        public IEnumerable<Vector2Int> Build()
        {
            return range;
        }
    }
}