using manager_retals.Core.Enums;

namespace manager_retals.Core.Strategies.RentalPlanCalculator
{
    public class FifteenDaysRentalStrategy : IRentalPlanStrategy
    {
        public RentalPlan Plan => RentalPlan.FifteenDays;

        public decimal GetDailyPrice() => 28.00m;
        public int GetDays() => 15;
        public decimal GetTotalPrice() => GetDays() * GetDailyPrice();
    }
}
