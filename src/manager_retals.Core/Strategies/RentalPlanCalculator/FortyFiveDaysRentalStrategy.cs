using manager_retals.Core.Enums;

namespace manager_retals.Core.Strategies.RentalPlanCalculator
{
    public class FortyFiveDaysRentalStrategy : IRentalPlanStrategy
    {
        public RentalPlan Plan => RentalPlan.FortyFiveDays;

        public decimal GetDailyPrice() => 20.00m;
        public int GetDays() => 45;
        public decimal GetTotalPrice() => GetDays() * GetDailyPrice();
    }
}
