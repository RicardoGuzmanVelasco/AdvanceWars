using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AdvanceWars.Tests.Builders
{
    public class RangeOfBuilder
    {
        private List<Vector2Int> range = new List<Vector2Int>();

        #region ObjectMothers
        public static RangeOfBuilder RangeOf() => new RangeOfBuilder();
        #endregion

        public RangeOfBuilder WithStructure(string rangeOfAsVerbatimString)
        {
            var rows = rangeOfAsVerbatimString.Replace("\r", string.Empty).Split("\n");

            // This is required because Vector2Int grows from bottom up: the first
            // row is the one at the bottom, not the top most. Otherwise, row index
            // of RangeOf is calculated inverted.
            rows = rows.Reverse().ToArray();

            for(var y = 0; y < rows.Length; y++)
            {
                var row = rows[y].Trim(' ').Split(' ').ToArray();

                for(var x = 0; x < row.Length; x++)
                    if(row[x] is "X")
                        range.Add(new Vector2Int(x, y));
            }

            return this;
        }

        public IEnumerable<Vector2Int> Build()
        {
            return range;
        }
    }
}