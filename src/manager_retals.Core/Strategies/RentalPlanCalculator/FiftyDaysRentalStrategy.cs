using manager_retals.Core.Enums;

namespace manager_retals.Core.Strategies.RentalPlanCalculator
{
    public class FiftyDaysRentalStrategy : IRentalPlanStrategy
    {
        public RentalPlan Plan => RentalPlan.FiftyDays;

        public decimal GetDailyPrice() => 18.00m;
        public int GetDays() => 50;
        public decimal GetTotalPrice() => GetDays() * GetDailyPrice();
    }
}
