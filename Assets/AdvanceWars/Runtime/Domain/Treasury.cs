using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Domain
{
    public class Treasury
    {
        public int WarFunds { get; private set; }

        public Treasury(int initialFunds = 0)
        {
            Require(initialFunds).Not.Negative();
            
            WarFunds = initialFunds;
        }
        
        public bool CanAfford(int amount) => WarFunds >= amount;

        public void Earn(int amount)
        {
            Require(amount).Positive();
            
            WarFunds += amount;
        }

        public void Spend(int amount)
        {
            Require(amount).Positive();
            Require(WarFunds - amount).Not.Negative();

            WarFunds -= amount;
        }

    }
}