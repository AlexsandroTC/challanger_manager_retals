using manager_retals.Core.Enums;

namespace manager_retals.Core.Strategies.RentalPlanCalculator
{
    public class ThirtyDaysRentalStrategy : IRentalPlanStrategy
    {
        public RentalPlan Plan => RentalPlan.ThirtyDays;

        public decimal GetDailyPrice() => 22.00m;
        public int GetDays() => 30;
        public decimal GetTotalPrice() => GetDays() * GetDailyPrice();
    }
}
