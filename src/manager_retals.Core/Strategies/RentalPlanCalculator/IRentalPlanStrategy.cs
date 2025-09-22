using manager_retals.Core.Enums;

namespace manager_retals.Core.Strategies.RentalPlanCalculator
{
    public interface IRentalPlanStrategy
    {
        RentalPlan Plan { get; }
        decimal GetDailyPrice();
        int GetDays();
        decimal GetTotalPrice();
    }
}
