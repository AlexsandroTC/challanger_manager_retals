using manager_retals.Core.Enums;

namespace manager_retals.Core.Strategies.RentalPlanCalculator
{
    public class SevenDaysRentalStrategy : IRentalPlanStrategy
    {
        public RentalPlan Plan => RentalPlan.SevenDays;

        public decimal GetDailyPrice() => 30.00m;
        public int GetDays() => 7;
        public decimal GetTotalPrice() => GetDays() * GetDailyPrice();
    }
}
