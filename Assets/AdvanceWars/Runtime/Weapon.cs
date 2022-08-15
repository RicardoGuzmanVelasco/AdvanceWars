using System.Collections.Generic;
using System.Linq;
using static RGV.DesignByContract.Runtime.Precondition;

namespace AdvanceWars.Runtime
{
    public class Weapon
    {
        readonly Dictionary<Unit, int> damages;

        public Weapon(Dictionary<Unit, int> damages)
        {
            Require(damages.Values.All(x => x > 0)).True();
            this.damages = damages;
        }

        public int BaseDamageTo(Unit target)
        {
            return damages.ContainsKey(target) ? damages[target] : 0;
        }
    }
}