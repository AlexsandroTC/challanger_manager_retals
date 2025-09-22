using manager_retals.Core.Enums;

namespace manager_retals.Core.Services
{
    public static class ReturnRentalCalculationService
    {
        private static readonly Dictionary<RentalPlan, decimal> _prices = new()
        {
            { RentalPlan.SevenDays, 30.00m },
            { RentalPlan.FifteenDays, 28.00m },
            { RentalPlan.ThirtyDays, 22.00m },
            { RentalPlan.FortyFiveDays, 20.00m },
            { RentalPlan.FiftyDays, 18.00m }
        };

        private static readonly Dictionary<RentalPlan, decimal> _penaltyRate = new()
        {
            { RentalPlan.SevenDays, 0.20m },
            { RentalPlan.FifteenDays, 0.40m }
        };

        public static decimal GetDailyPrice(RentalPlan plan) => _prices[plan];

        public static decimal GetPenaltyRate(RentalPlan plan) => _penaltyRate.ContainsKey(plan) ? _penaltyRate[plan] : 0m;

        public static decimal CalculateFinalPrice(DateTime actualReturn, DateTime newReturnDate, RentalPlan plan, decimal totalPrice)
        {
            if (actualReturn < newReturnDate)
            {
                int unusedDays = (newReturnDate - actualReturn).Days;
                decimal penaltyRate = GetPenaltyRate(plan);

                decimal unusedValue = unusedDays * GetDailyPrice(plan);
                decimal penalty = unusedValue * penaltyRate;

                return totalPrice - unusedValue + penalty;
            }
            else if (actualReturn > newReturnDate)
            {
                int extraDays = (actualReturn - newReturnDate).Days;
                decimal extraFee = extraDays * 50m;

                return totalPrice + extraFee;
            }

            return totalPrice;
        }
    }
}