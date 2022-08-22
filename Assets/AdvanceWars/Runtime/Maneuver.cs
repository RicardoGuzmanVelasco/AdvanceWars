using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime
{
    public class Maneuver
    {
        public Maneuver([NotNull] Battalion performer)
        {
            Require(performer.Equals(Battalion.Null)).False();
            Performer = performer;
        }

        public Battalion Performer { get; set; }
    }
}