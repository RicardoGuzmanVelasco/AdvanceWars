using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public partial record Weapon
    {
        readonly Dictionary<Armor, int> damages;

        public Weapon([NotNull] Dictionary<Armor, int> damages)
        {
            Require(damages.Values.All(x => x > 0)).True();
            this.damages = damages;
        }

        public int BaseDamageTo([NotNull] Armor target)
        {
            return damages.ContainsKey(target) ? damages[target] : 0;
        }

        public override string ToString()
        {
            return damages.GetHashCode().ToString();
        }
    }
}