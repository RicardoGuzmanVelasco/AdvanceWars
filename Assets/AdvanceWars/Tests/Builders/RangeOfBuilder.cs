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
            var rows = rangeOfAsVerbatimString.Split("\n");

            for (var i = 0; i < rows.Length; i++)
            {
                var row = rows[i].Where(x => x != ' ').ToArray();

                for (var j = 0; j < row.Length; j++)
                    if(row[j] is 'X')
                        range.Add(new Vector2Int(j, i));
            }

            return this;
        }

        public IEnumerable<Vector2Int> Build()
        {
            return range;
        }
    }
}