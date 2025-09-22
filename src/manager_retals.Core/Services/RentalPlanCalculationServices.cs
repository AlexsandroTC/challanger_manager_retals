using manager_retals.Core.Enums;
using manager_retals.Core.Strategies.RentalPlanCalculator;

namespace manager_retals.Core.Services
{
    public class RentalPlanCalculationServices
    {
        private readonly Dictionary<RentalPlan, IRentalPlanStrategy> _strategies;

        public RentalPlanCalculationServices(IEnumerable<IRentalPlanStrategy> strategies)
        {
            _strategies = strategies.ToDictionary(s => s.Plan, s => s);
        }

        public IRentalPlanStrategy GetStrategy(RentalPlan plan)
        {
            if (!_strategies.TryGetValue(plan, out var strategy))
                throw new ArgumentException($"No strategy found for plan {plan}");

            return strategy;
        }
    }
}
