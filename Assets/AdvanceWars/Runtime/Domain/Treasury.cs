using AdvanceWars.Runtime.Domain.Troops;
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
        
        public bool CanAfford(Price amount) => WarFunds >= amount;
        public bool CanAfford(Unit unit) => WarFunds >= unit.Price;

        public void Earn(int amount)
        {
            Require(amount).Positive();
            
            WarFunds += amount;
        }

        public void Spend(int amount)
        {
            Require(amount).Positive();
            Require(WarFunds).GreaterOrEqualThan(amount);;

            WarFunds -= amount;
        }

        public void PayRecruitment(Unit unit)
        {
            if (unit.Price > 0)
            {
                Spend(unit.Price);
            }
        }
    }
}