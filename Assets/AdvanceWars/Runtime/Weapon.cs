using System.Collections.Generic;
using System.Linq;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Weapon
    {
        readonly Dictionary<Armor, int> damages;

        public Weapon(Dictionary<Armor, int> damages)
        {
            Require(damages.Values.All(x => x > 0)).True();
            this.damages = damages;
        }

        public int BaseDamageTo(Armor target)
        {
            return damages.ContainsKey(target) ? damages[target] : 0;
        }
    }
}